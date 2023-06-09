##### SETTINGS #####
training_data_path = r"C:\Users\timh\Downloads\tah\chess-ai\training.jsonl" # path to the .jsonl file that contains the training data
training_set_batch_size = 500
model_output_directory = r"C:\Users\timh\Downloads\tah\chess-ai\models" # path to the parent directory you want the models to be dumped into when they are saved

example_cap:int =  None # if you want to limit the number of examples the model trains on, enter it here. Set it to "None" (null) if you want it to go through all of the data in the training file
save_model_ever_seconds:int = 60 * 120
####################

import tensorflow as tf
import numpy
import json
import uuid
import os
import datetime

# count the number of lines (training examples) in this file
print("Counting examples in training data... ")
f = open(training_data_path)
total_example_count = 0
for line in f:
    total_example_count = total_example_count + 1
total_example_count_formatted:str = format(total_example_count, ",d")
print(total_example_count_formatted + " examples!")



# Save a tensorflow keras sequential model to a new directory in a parent directory
def save_model(model:tf.keras.Sequential, model_number:int = None) -> None:

    # number part
    num_part:str = ""
    if (model_number != None):
        num_part = str(model_number)

    # prepare guid
    g = uuid.uuid4()
    s:str = str(g)
    s = s.replace("-", "")
    s = s[:5]

    path:str = model_output_directory + "\\model" + num_part + "-" + s + "\\"
    os.mkdir(path)

    model.save(path)




model = tf.keras.Sequential()
model.add(tf.keras.layers.Dense(854)) # innputs
model.add(tf.keras.layers.Dense(3000, "relu"))
model.add(tf.keras.layers.Dense(5000, "relu"))
model.add(tf.keras.layers.Dense(4500, "relu"))
model.add(tf.keras.layers.Dense(3000, "relu"))
model.add(tf.keras.layers.Dense(1860)) # outputs
model.compile("adam", "mean_squared_error")

# TRAIN!
f.seek(0) # go back to the start of the file again
input_sets = []
output_sets = []
eof = False
stop = False
on_model_number:int = 1
on_line = 0
model_last_saved_at_utc:datetime.datetime = datetime.datetime.utcnow()
while eof == False and stop == False:

    line = f.readline()

    if not line:
        eof = True
    elif line != "":

        # percent complete calculation
        on_line = on_line + 1
        if example_cap != None:
            to_do = example_cap
        else:
            to_do = total_example_count
        percent_complete:float = on_line / to_do
        percent_complete_str:str = f'{percent_complete:.3%}'
        print("Preparing example " + format(on_line, ",d") + " (" + percent_complete_str + ")... ")

        iopair = json.loads(line)

        inputs_condensed = iopair["inputs"]
        output_neuron_index = iopair["output"]

        # Convert the condensed input data to the full array for training
        inputs = []
        for x in range(854):
            inputs.append(0.0)
        for i in inputs_condensed:
            inputs[i] = 1.0
    
        # Convert the condensed output data to the full array for training
        outputs = []
        for x in range(1860):
            outputs.append(0.0)
        outputs[output_neuron_index] = 1.0

        # add it to the batch set we will train once it fills up enough
        input_sets.append(inputs)
        output_sets.append(outputs)

    # are we beyond the training example cap limit? if we are, make the command to stop
    if example_cap != None:
        if on_line >= example_cap:
            print("Training example cap of " + str(example_cap) + " reached!")
            stop = True
    
    # Is it time to save? i.e. hopper too full, end of file reached, or commanded to stop
    if len(input_sets) >= training_set_batch_size or eof or stop: # if the hopper has reached the desired batch size OR we have hit the end of file and need to perform the one last training round on what is in the hopper

        # prepare for numpy
        inputs_ = numpy.array(input_sets)
        outputs_ = numpy.array(output_sets)

        # train
        print("Training model on " + str(len(input_sets)) + " examples... ")
        model.fit(inputs_, outputs_, epochs=5)
        print("Training complete!")

        # dump the input and output sets (hoppers)
        input_sets.clear()
        output_sets.clear()

    # is it time for me to save the model again?
    time_since_last_save:datetime.timedelta = datetime.datetime.utcnow() - model_last_saved_at_utc
    if time_since_last_save.total_seconds() > save_model_ever_seconds:
        print("It is time to save! Saving... ")
        save_model(model, on_model_number)
        print("Saved!")
        on_model_number = on_model_number + 1
        model_last_saved_at_utc = datetime.datetime.utcnow()


# close the file
f.close()
print("Training data file closed!")

# save
print("Saving model... ")
save_model(model, on_model_number)