model_dir = r"C:\Users\timh\Downloads\tah\chess-ai\models\model7-923d0f8856dc4d75ada0670c15060729"


import tensorflow as tf
import numpy
import json

model:tf.keras.Sequential = tf.keras.models.load_model(model_dir)

while True:
    print("Paste the input array of floats, in JSON, below:")
    print()
    input_json:str = input("Paste here >")
    print()
    inputs = json.loads(input_json)

    i = numpy.array([inputs])
    o = model.predict(i)[0]

    # select the highest output neuron
    winner_index:int = 0
    on_index:int = 0
    for val in o:
        if val > o[winner_index]:
            winner_index = on_index
        on_index = on_index + 1

    print("Output neuron index '" + str(winner_index) + "' selected!")
    print("----")
