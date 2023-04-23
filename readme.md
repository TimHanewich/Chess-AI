## Input Structure Explanation
There are 64 squares on a chess board. Each square's status can be one of the following:
- empty
- white pawn
- white knight
- white bishop
- white rook
- white queen
- white king
- black pawn
- black knight
- black bishop
- black rook
- black queen
- black king

That is 13 potential states for 64 squares, so a total of 832 potential board states. Thus, to represent all of the pieces (or lack of pieces) on the board, 832 input neurons are needed.

In total, the following must also be represented:
- **832 neurons** - represent board state (pieces, or lack of pieces, on the board)
- **2 neurons** - Who's turn is it to move? (white or black?)
- **4 neurons** - Castling availability (white kingside, white queenside, black kingside, black queenside)
- **16 neurons** - En passant target square, if available (there are only 16 squares that can ever be available for en passant target)

Thus, the input structure will have the **854** neurons to represent the entire state at any given point in a game.

## Output Structure Explanation
We will think of a move as being from *position 1* to *position 2*. In other words, we'll think of a move as being a move of a piece from a current position (square) on the board to another position (square) on the board. For example:
- A1 to A2
- A1 to A3
- A1 to B1
- A1 to C3
Each square has 63 potential positions it can move to. Thus, there are 64x63 potential from __ to __ scenarios: 4,032. 

**However, we know certain moves are illegal and NEVER will be possible!**. For example, moving from A2 to G5? That will *never* be possible. So we have will filter the list of potential moves found above (in the 4,032 list) to moves that will only ever show up in game. 

We arrive at 1,792 possible "standard" moves from point A to point B. This is testing for all pieces in every position (except the pawns on the 1st or 8th rank as this is impossible - they'd need to be promoted). You can find a serialized list of the moves [here](./standard_moves.json). Use the enum `Position` in the TimHanewich.Chess nuget package to deserialize this list.

In addition to these simple scenarios, there are also special moves:

- White king-side castling
- White queen-side castling
- Black king-side castling
- Black queen-side castling

Finally, there are also pawn promotions. For example:

- White A pawn promotion to queen
- White B pawn promotion to queen
- White C pawn promotion to queen
- White D pawn promotion to queen
- White E pawn promotion to queen
- White F pawn promotion to queen
- White G pawn promotion to queen
- White H pawn promotion to queen
- Black A pawn promotion to queen
- Black B pawn promotion to queen
- Black C pawn promotion to queen
- Black D pawn promotion to queen
- Black E pawn promotion to queen
- Black F pawn promotion to queen
- Black G pawn promotion to queen
- Black H pawn promotion to queen
- White A pawn promotion to rook
- White B pawn promotion to rook
- White C pawn promotion to rook
- White D pawn promotion to rook
- etc..

There are 8 pawns for each side, so a total of 16 pawns. And each pawn can be promoted to one of 4 pieces. So there are 16x4 different scenarios for a pawn promotion, or a total of 64 moves.

If we were trying to represent each **possible** move, we would thus need the following:
- 1,792 for standard position __ to position __
- 4 for castling (white king-side, white queen-side, black king-side, black queen-side)
- 64 for each unique pawn promotion

A total of **1,860** output neurons.

## Input Structure
|Neuron Index|Description|
|-|-|
|0|A1 is empty|
|1|A1 is white pawn|
|2|A1 is white knight|
|3|A1 is white bishop|
|4|A1 is white rook|
|5|A1 is white queen|
|6|A1 is white king|
|7|A1 is black pawn|
|8|A1 is black knight|
|9|A1 is black bishop|
|10|A1 is black rook|
|11|A1 is black queen|
|12|A1 is black king|
|13|A2 is empty|
|14|A2 is white pawn|
|15|A2 is white knight|
|16|A2 is white bishop|
|17|A2 is white rook|
|18|A2 is white queen|
|19|A2 is white king|
|20|A2 is black pawn|
|21|A2 is black knight|
|22|A2 is black bishop|
|23|A2 is black rook|
|24|A2 is black queen|
|25|A2 is black king|
|26|A3 is empty|
|27|A3 is white pawn|
|28|A3 is white knight|
|29|A3 is white bishop|
|30|A3 is white rook|
|31|A3 is white queen|
|32|A3 is white king|
|33|A3 is black pawn|
|34|A3 is black knight|
|35|A3 is black bishop|
|36|A3 is black rook|
|37|A3 is black queen|
|38|A3 is black king|
|39|A4 is empty|
|40|A4 is white pawn|
|41|A4 is white knight|
|42|A4 is white bishop|
|43|A4 is white rook|
|44|A4 is white queen|
|45|A4 is white king|
|46|A4 is black pawn|
|47|A4 is black knight|
|48|A4 is black bishop|
|49|A4 is black rook|
|50|A4 is black queen|
|51|A4 is black king|
|52|A5 is empty|
|53|A5 is white pawn|
|54|A5 is white knight|
|55|A5 is white bishop|
|56|A5 is white rook|
|57|A5 is white queen|
|58|A5 is white king|
|59|A5 is black pawn|
|60|A5 is black knight|
|61|A5 is black bishop|
|62|A5 is black rook|
|63|A5 is black queen|
|64|A5 is black king|
|65|A6 is empty|
|66|A6 is white pawn|
|67|A6 is white knight|
|68|A6 is white bishop|
|69|A6 is white rook|
|70|A6 is white queen|
|71|A6 is white king|
|72|A6 is black pawn|
|73|A6 is black knight|
|74|A6 is black bishop|
|75|A6 is black rook|
|76|A6 is black queen|
|77|A6 is black king|
|78|A7 is empty|
|79|A7 is white pawn|
|80|A7 is white knight|
|81|A7 is white bishop|
|82|A7 is white rook|
|83|A7 is white queen|
|84|A7 is white king|
|85|A7 is black pawn|
|86|A7 is black knight|
|87|A7 is black bishop|
|88|A7 is black rook|
|89|A7 is black queen|
|90|A7 is black king|
|91|A8 is empty|
|92|A8 is white pawn|
|93|A8 is white knight|
|94|A8 is white bishop|
|95|A8 is white rook|
|96|A8 is white queen|
|97|A8 is white king|
|98|A8 is black pawn|
|99|A8 is black knight|
|100|A8 is black bishop|
|101|A8 is black rook|
|102|A8 is black queen|
|103|A8 is black king|
|104|B1 is empty|
|105|B1 is white pawn|
|106|B1 is white knight|
|107|B1 is white bishop|
|108|B1 is white rook|
|109|B1 is white queen|
|110|B1 is white king|
|111|B1 is black pawn|
|112|B1 is black knight|
|113|B1 is black bishop|
|114|B1 is black rook|
|115|B1 is black queen|
|116|B1 is black king|
|117|B2 is empty|
|118|B2 is white pawn|
|119|B2 is white knight|
|120|B2 is white bishop|
|121|B2 is white rook|
|122|B2 is white queen|
|123|B2 is white king|
|124|B2 is black pawn|
|125|B2 is black knight|
|126|B2 is black bishop|
|127|B2 is black rook|
|128|B2 is black queen|
|129|B2 is black king|
|130|B3 is empty|
|131|B3 is white pawn|
|132|B3 is white knight|
|133|B3 is white bishop|
|134|B3 is white rook|
|135|B3 is white queen|
|136|B3 is white king|
|137|B3 is black pawn|
|138|B3 is black knight|
|139|B3 is black bishop|
|140|B3 is black rook|
|141|B3 is black queen|
|142|B3 is black king|
|143|B4 is empty|
|144|B4 is white pawn|
|145|B4 is white knight|
|146|B4 is white bishop|
|147|B4 is white rook|
|148|B4 is white queen|
|149|B4 is white king|
|150|B4 is black pawn|
|151|B4 is black knight|
|152|B4 is black bishop|
|153|B4 is black rook|
|154|B4 is black queen|
|155|B4 is black king|
|156|B5 is empty|
|157|B5 is white pawn|
|158|B5 is white knight|
|159|B5 is white bishop|
|160|B5 is white rook|
|161|B5 is white queen|
|162|B5 is white king|
|163|B5 is black pawn|
|164|B5 is black knight|
|165|B5 is black bishop|
|166|B5 is black rook|
|167|B5 is black queen|
|168|B5 is black king|
|169|B6 is empty|
|170|B6 is white pawn|
|171|B6 is white knight|
|172|B6 is white bishop|
|173|B6 is white rook|
|174|B6 is white queen|
|175|B6 is white king|
|176|B6 is black pawn|
|177|B6 is black knight|
|178|B6 is black bishop|
|179|B6 is black rook|
|180|B6 is black queen|
|181|B6 is black king|
|182|B7 is empty|
|183|B7 is white pawn|
|184|B7 is white knight|
|185|B7 is white bishop|
|186|B7 is white rook|
|187|B7 is white queen|
|188|B7 is white king|
|189|B7 is black pawn|
|190|B7 is black knight|
|191|B7 is black bishop|
|192|B7 is black rook|
|193|B7 is black queen|
|194|B7 is black king|
|195|B8 is empty|
|196|B8 is white pawn|
|197|B8 is white knight|
|198|B8 is white bishop|
|199|B8 is white rook|
|200|B8 is white queen|
|201|B8 is white king|
|202|B8 is black pawn|
|203|B8 is black knight|
|204|B8 is black bishop|
|205|B8 is black rook|
|206|B8 is black queen|
|207|B8 is black king|
|208|C1 is empty|
|209|C1 is white pawn|
|210|C1 is white knight|
|211|C1 is white bishop|
|212|C1 is white rook|
|213|C1 is white queen|
|214|C1 is white king|
|215|C1 is black pawn|
|216|C1 is black knight|
|217|C1 is black bishop|
|218|C1 is black rook|
|219|C1 is black queen|
|220|C1 is black king|
|221|C2 is empty|
|222|C2 is white pawn|
|223|C2 is white knight|
|224|C2 is white bishop|
|225|C2 is white rook|
|226|C2 is white queen|
|227|C2 is white king|
|228|C2 is black pawn|
|229|C2 is black knight|
|230|C2 is black bishop|
|231|C2 is black rook|
|232|C2 is black queen|
|233|C2 is black king|
|234|C3 is empty|
|235|C3 is white pawn|
|236|C3 is white knight|
|237|C3 is white bishop|
|238|C3 is white rook|
|239|C3 is white queen|
|240|C3 is white king|
|241|C3 is black pawn|
|242|C3 is black knight|
|243|C3 is black bishop|
|244|C3 is black rook|
|245|C3 is black queen|
|246|C3 is black king|
|247|C4 is empty|
|248|C4 is white pawn|
|249|C4 is white knight|
|250|C4 is white bishop|
|251|C4 is white rook|
|252|C4 is white queen|
|253|C4 is white king|
|254|C4 is black pawn|
|255|C4 is black knight|
|256|C4 is black bishop|
|257|C4 is black rook|
|258|C4 is black queen|
|259|C4 is black king|
|260|C5 is empty|
|261|C5 is white pawn|
|262|C5 is white knight|
|263|C5 is white bishop|
|264|C5 is white rook|
|265|C5 is white queen|
|266|C5 is white king|
|267|C5 is black pawn|
|268|C5 is black knight|
|269|C5 is black bishop|
|270|C5 is black rook|
|271|C5 is black queen|
|272|C5 is black king|
|273|C6 is empty|
|274|C6 is white pawn|
|275|C6 is white knight|
|276|C6 is white bishop|
|277|C6 is white rook|
|278|C6 is white queen|
|279|C6 is white king|
|280|C6 is black pawn|
|281|C6 is black knight|
|282|C6 is black bishop|
|283|C6 is black rook|
|284|C6 is black queen|
|285|C6 is black king|
|286|C7 is empty|
|287|C7 is white pawn|
|288|C7 is white knight|
|289|C7 is white bishop|
|290|C7 is white rook|
|291|C7 is white queen|
|292|C7 is white king|
|293|C7 is black pawn|
|294|C7 is black knight|
|295|C7 is black bishop|
|296|C7 is black rook|
|297|C7 is black queen|
|298|C7 is black king|
|299|C8 is empty|
|300|C8 is white pawn|
|301|C8 is white knight|
|302|C8 is white bishop|
|303|C8 is white rook|
|304|C8 is white queen|
|305|C8 is white king|
|306|C8 is black pawn|
|307|C8 is black knight|
|308|C8 is black bishop|
|309|C8 is black rook|
|310|C8 is black queen|
|311|C8 is black king|
|312|D1 is empty|
|313|D1 is white pawn|
|314|D1 is white knight|
|315|D1 is white bishop|
|316|D1 is white rook|
|317|D1 is white queen|
|318|D1 is white king|
|319|D1 is black pawn|
|320|D1 is black knight|
|321|D1 is black bishop|
|322|D1 is black rook|
|323|D1 is black queen|
|324|D1 is black king|
|325|D2 is empty|
|326|D2 is white pawn|
|327|D2 is white knight|
|328|D2 is white bishop|
|329|D2 is white rook|
|330|D2 is white queen|
|331|D2 is white king|
|332|D2 is black pawn|
|333|D2 is black knight|
|334|D2 is black bishop|
|335|D2 is black rook|
|336|D2 is black queen|
|337|D2 is black king|
|338|D3 is empty|
|339|D3 is white pawn|
|340|D3 is white knight|
|341|D3 is white bishop|
|342|D3 is white rook|
|343|D3 is white queen|
|344|D3 is white king|
|345|D3 is black pawn|
|346|D3 is black knight|
|347|D3 is black bishop|
|348|D3 is black rook|
|349|D3 is black queen|
|350|D3 is black king|
|351|D4 is empty|
|352|D4 is white pawn|
|353|D4 is white knight|
|354|D4 is white bishop|
|355|D4 is white rook|
|356|D4 is white queen|
|357|D4 is white king|
|358|D4 is black pawn|
|359|D4 is black knight|
|360|D4 is black bishop|
|361|D4 is black rook|
|362|D4 is black queen|
|363|D4 is black king|
|364|D5 is empty|
|365|D5 is white pawn|
|366|D5 is white knight|
|367|D5 is white bishop|
|368|D5 is white rook|
|369|D5 is white queen|
|370|D5 is white king|
|371|D5 is black pawn|
|372|D5 is black knight|
|373|D5 is black bishop|
|374|D5 is black rook|
|375|D5 is black queen|
|376|D5 is black king|
|377|D6 is empty|
|378|D6 is white pawn|
|379|D6 is white knight|
|380|D6 is white bishop|
|381|D6 is white rook|
|382|D6 is white queen|
|383|D6 is white king|
|384|D6 is black pawn|
|385|D6 is black knight|
|386|D6 is black bishop|
|387|D6 is black rook|
|388|D6 is black queen|
|389|D6 is black king|
|390|D7 is empty|
|391|D7 is white pawn|
|392|D7 is white knight|
|393|D7 is white bishop|
|394|D7 is white rook|
|395|D7 is white queen|
|396|D7 is white king|
|397|D7 is black pawn|
|398|D7 is black knight|
|399|D7 is black bishop|
|400|D7 is black rook|
|401|D7 is black queen|
|402|D7 is black king|
|403|D8 is empty|
|404|D8 is white pawn|
|405|D8 is white knight|
|406|D8 is white bishop|
|407|D8 is white rook|
|408|D8 is white queen|
|409|D8 is white king|
|410|D8 is black pawn|
|411|D8 is black knight|
|412|D8 is black bishop|
|413|D8 is black rook|
|414|D8 is black queen|
|415|D8 is black king|
|416|E1 is empty|
|417|E1 is white pawn|
|418|E1 is white knight|
|419|E1 is white bishop|
|420|E1 is white rook|
|421|E1 is white queen|
|422|E1 is white king|
|423|E1 is black pawn|
|424|E1 is black knight|
|425|E1 is black bishop|
|426|E1 is black rook|
|427|E1 is black queen|
|428|E1 is black king|
|429|E2 is empty|
|430|E2 is white pawn|
|431|E2 is white knight|
|432|E2 is white bishop|
|433|E2 is white rook|
|434|E2 is white queen|
|435|E2 is white king|
|436|E2 is black pawn|
|437|E2 is black knight|
|438|E2 is black bishop|
|439|E2 is black rook|
|440|E2 is black queen|
|441|E2 is black king|
|442|E3 is empty|
|443|E3 is white pawn|
|444|E3 is white knight|
|445|E3 is white bishop|
|446|E3 is white rook|
|447|E3 is white queen|
|448|E3 is white king|
|449|E3 is black pawn|
|450|E3 is black knight|
|451|E3 is black bishop|
|452|E3 is black rook|
|453|E3 is black queen|
|454|E3 is black king|
|455|E4 is empty|
|456|E4 is white pawn|
|457|E4 is white knight|
|458|E4 is white bishop|
|459|E4 is white rook|
|460|E4 is white queen|
|461|E4 is white king|
|462|E4 is black pawn|
|463|E4 is black knight|
|464|E4 is black bishop|
|465|E4 is black rook|
|466|E4 is black queen|
|467|E4 is black king|
|468|E5 is empty|
|469|E5 is white pawn|
|470|E5 is white knight|
|471|E5 is white bishop|
|472|E5 is white rook|
|473|E5 is white queen|
|474|E5 is white king|
|475|E5 is black pawn|
|476|E5 is black knight|
|477|E5 is black bishop|
|478|E5 is black rook|
|479|E5 is black queen|
|480|E5 is black king|
|481|E6 is empty|
|482|E6 is white pawn|
|483|E6 is white knight|
|484|E6 is white bishop|
|485|E6 is white rook|
|486|E6 is white queen|
|487|E6 is white king|
|488|E6 is black pawn|
|489|E6 is black knight|
|490|E6 is black bishop|
|491|E6 is black rook|
|492|E6 is black queen|
|493|E6 is black king|
|494|E7 is empty|
|495|E7 is white pawn|
|496|E7 is white knight|
|497|E7 is white bishop|
|498|E7 is white rook|
|499|E7 is white queen|
|500|E7 is white king|
|501|E7 is black pawn|
|502|E7 is black knight|
|503|E7 is black bishop|
|504|E7 is black rook|
|505|E7 is black queen|
|506|E7 is black king|
|507|E8 is empty|
|508|E8 is white pawn|
|509|E8 is white knight|
|510|E8 is white bishop|
|511|E8 is white rook|
|512|E8 is white queen|
|513|E8 is white king|
|514|E8 is black pawn|
|515|E8 is black knight|
|516|E8 is black bishop|
|517|E8 is black rook|
|518|E8 is black queen|
|519|E8 is black king|
|520|F1 is empty|
|521|F1 is white pawn|
|522|F1 is white knight|
|523|F1 is white bishop|
|524|F1 is white rook|
|525|F1 is white queen|
|526|F1 is white king|
|527|F1 is black pawn|
|528|F1 is black knight|
|529|F1 is black bishop|
|530|F1 is black rook|
|531|F1 is black queen|
|532|F1 is black king|
|533|F2 is empty|
|534|F2 is white pawn|
|535|F2 is white knight|
|536|F2 is white bishop|
|537|F2 is white rook|
|538|F2 is white queen|
|539|F2 is white king|
|540|F2 is black pawn|
|541|F2 is black knight|
|542|F2 is black bishop|
|543|F2 is black rook|
|544|F2 is black queen|
|545|F2 is black king|
|546|F3 is empty|
|547|F3 is white pawn|
|548|F3 is white knight|
|549|F3 is white bishop|
|550|F3 is white rook|
|551|F3 is white queen|
|552|F3 is white king|
|553|F3 is black pawn|
|554|F3 is black knight|
|555|F3 is black bishop|
|556|F3 is black rook|
|557|F3 is black queen|
|558|F3 is black king|
|559|F4 is empty|
|560|F4 is white pawn|
|561|F4 is white knight|
|562|F4 is white bishop|
|563|F4 is white rook|
|564|F4 is white queen|
|565|F4 is white king|
|566|F4 is black pawn|
|567|F4 is black knight|
|568|F4 is black bishop|
|569|F4 is black rook|
|570|F4 is black queen|
|571|F4 is black king|
|572|F5 is empty|
|573|F5 is white pawn|
|574|F5 is white knight|
|575|F5 is white bishop|
|576|F5 is white rook|
|577|F5 is white queen|
|578|F5 is white king|
|579|F5 is black pawn|
|580|F5 is black knight|
|581|F5 is black bishop|
|582|F5 is black rook|
|583|F5 is black queen|
|584|F5 is black king|
|585|F6 is empty|
|586|F6 is white pawn|
|587|F6 is white knight|
|588|F6 is white bishop|
|589|F6 is white rook|
|590|F6 is white queen|
|591|F6 is white king|
|592|F6 is black pawn|
|593|F6 is black knight|
|594|F6 is black bishop|
|595|F6 is black rook|
|596|F6 is black queen|
|597|F6 is black king|
|598|F7 is empty|
|599|F7 is white pawn|
|600|F7 is white knight|
|601|F7 is white bishop|
|602|F7 is white rook|
|603|F7 is white queen|
|604|F7 is white king|
|605|F7 is black pawn|
|606|F7 is black knight|
|607|F7 is black bishop|
|608|F7 is black rook|
|609|F7 is black queen|
|610|F7 is black king|
|611|F8 is empty|
|612|F8 is white pawn|
|613|F8 is white knight|
|614|F8 is white bishop|
|615|F8 is white rook|
|616|F8 is white queen|
|617|F8 is white king|
|618|F8 is black pawn|
|619|F8 is black knight|
|620|F8 is black bishop|
|621|F8 is black rook|
|622|F8 is black queen|
|623|F8 is black king|
|624|G1 is empty|
|625|G1 is white pawn|
|626|G1 is white knight|
|627|G1 is white bishop|
|628|G1 is white rook|
|629|G1 is white queen|
|630|G1 is white king|
|631|G1 is black pawn|
|632|G1 is black knight|
|633|G1 is black bishop|
|634|G1 is black rook|
|635|G1 is black queen|
|636|G1 is black king|
|637|G2 is empty|
|638|G2 is white pawn|
|639|G2 is white knight|
|640|G2 is white bishop|
|641|G2 is white rook|
|642|G2 is white queen|
|643|G2 is white king|
|644|G2 is black pawn|
|645|G2 is black knight|
|646|G2 is black bishop|
|647|G2 is black rook|
|648|G2 is black queen|
|649|G2 is black king|
|650|G3 is empty|
|651|G3 is white pawn|
|652|G3 is white knight|
|653|G3 is white bishop|
|654|G3 is white rook|
|655|G3 is white queen|
|656|G3 is white king|
|657|G3 is black pawn|
|658|G3 is black knight|
|659|G3 is black bishop|
|660|G3 is black rook|
|661|G3 is black queen|
|662|G3 is black king|
|663|G4 is empty|
|664|G4 is white pawn|
|665|G4 is white knight|
|666|G4 is white bishop|
|667|G4 is white rook|
|668|G4 is white queen|
|669|G4 is white king|
|670|G4 is black pawn|
|671|G4 is black knight|
|672|G4 is black bishop|
|673|G4 is black rook|
|674|G4 is black queen|
|675|G4 is black king|
|676|G5 is empty|
|677|G5 is white pawn|
|678|G5 is white knight|
|679|G5 is white bishop|
|680|G5 is white rook|
|681|G5 is white queen|
|682|G5 is white king|
|683|G5 is black pawn|
|684|G5 is black knight|
|685|G5 is black bishop|
|686|G5 is black rook|
|687|G5 is black queen|
|688|G5 is black king|
|689|G6 is empty|
|690|G6 is white pawn|
|691|G6 is white knight|
|692|G6 is white bishop|
|693|G6 is white rook|
|694|G6 is white queen|
|695|G6 is white king|
|696|G6 is black pawn|
|697|G6 is black knight|
|698|G6 is black bishop|
|699|G6 is black rook|
|700|G6 is black queen|
|701|G6 is black king|
|702|G7 is empty|
|703|G7 is white pawn|
|704|G7 is white knight|
|705|G7 is white bishop|
|706|G7 is white rook|
|707|G7 is white queen|
|708|G7 is white king|
|709|G7 is black pawn|
|710|G7 is black knight|
|711|G7 is black bishop|
|712|G7 is black rook|
|713|G7 is black queen|
|714|G7 is black king|
|715|G8 is empty|
|716|G8 is white pawn|
|717|G8 is white knight|
|718|G8 is white bishop|
|719|G8 is white rook|
|720|G8 is white queen|
|721|G8 is white king|
|722|G8 is black pawn|
|723|G8 is black knight|
|724|G8 is black bishop|
|725|G8 is black rook|
|726|G8 is black queen|
|727|G8 is black king|
|728|H1 is empty|
|729|H1 is white pawn|
|730|H1 is white knight|
|731|H1 is white bishop|
|732|H1 is white rook|
|733|H1 is white queen|
|734|H1 is white king|
|735|H1 is black pawn|
|736|H1 is black knight|
|737|H1 is black bishop|
|738|H1 is black rook|
|739|H1 is black queen|
|740|H1 is black king|
|741|H2 is empty|
|742|H2 is white pawn|
|743|H2 is white knight|
|744|H2 is white bishop|
|745|H2 is white rook|
|746|H2 is white queen|
|747|H2 is white king|
|748|H2 is black pawn|
|749|H2 is black knight|
|750|H2 is black bishop|
|751|H2 is black rook|
|752|H2 is black queen|
|753|H2 is black king|
|754|H3 is empty|
|755|H3 is white pawn|
|756|H3 is white knight|
|757|H3 is white bishop|
|758|H3 is white rook|
|759|H3 is white queen|
|760|H3 is white king|
|761|H3 is black pawn|
|762|H3 is black knight|
|763|H3 is black bishop|
|764|H3 is black rook|
|765|H3 is black queen|
|766|H3 is black king|
|767|H4 is empty|
|768|H4 is white pawn|
|769|H4 is white knight|
|770|H4 is white bishop|
|771|H4 is white rook|
|772|H4 is white queen|
|773|H4 is white king|
|774|H4 is black pawn|
|775|H4 is black knight|
|776|H4 is black bishop|
|777|H4 is black rook|
|778|H4 is black queen|
|779|H4 is black king|
|780|H5 is empty|
|781|H5 is white pawn|
|782|H5 is white knight|
|783|H5 is white bishop|
|784|H5 is white rook|
|785|H5 is white queen|
|786|H5 is white king|
|787|H5 is black pawn|
|788|H5 is black knight|
|789|H5 is black bishop|
|790|H5 is black rook|
|791|H5 is black queen|
|792|H5 is black king|
|793|H6 is empty|
|794|H6 is white pawn|
|795|H6 is white knight|
|796|H6 is white bishop|
|797|H6 is white rook|
|798|H6 is white queen|
|799|H6 is white king|
|800|H6 is black pawn|
|801|H6 is black knight|
|802|H6 is black bishop|
|803|H6 is black rook|
|804|H6 is black queen|
|805|H6 is black king|
|806|H7 is empty|
|807|H7 is white pawn|
|808|H7 is white knight|
|809|H7 is white bishop|
|810|H7 is white rook|
|811|H7 is white queen|
|812|H7 is white king|
|813|H7 is black pawn|
|814|H7 is black knight|
|815|H7 is black bishop|
|816|H7 is black rook|
|817|H7 is black queen|
|818|H7 is black king|
|819|H8 is empty|
|820|H8 is white pawn|
|821|H8 is white knight|
|822|H8 is white bishop|
|823|H8 is white rook|
|824|H8 is white queen|
|825|H8 is white king|
|826|H8 is black pawn|
|827|H8 is black knight|
|828|H8 is black bishop|
|829|H8 is black rook|
|830|H8 is black queen|
|831|H8 is black king|
|832|White to move next|
|833|Black to move next|
|834|White kingside castling available|
|835|White queenside castling available|
|836|Black kingside castling available|
|837|Black queenside castling available|
|838|En Passant target available on A3|
|839|En Passant target available on B3|
|840|En Passant target available on C3|
|841|En Passant target available on D3|
|842|En Passant target available on E3|
|843|En Passant target available on F3|
|844|En Passant target available on G3|
|845|En Passant target available on H3|
|846|En Passant target available on A6|
|847|En Passant target available on B6|
|848|En Passant target available on C6|
|849|En Passant target available on D6|
|850|En Passant target available on E6|
|851|En Passant target available on F6|
|852|En Passant target available on G6|
|853|En Passant target available on H6|

## Output Structure
|Neuron Index|Description|
|-|-|
|0|A1 to A2|
|1|A1 to A3|
|2|A1 to A4|
|3|A1 to A5|
|4|A1 to A6|
|5|A1 to A7|
|6|A1 to A8|
|7|A1 to B1|
|8|A1 to B2|
|9|A1 to B3|
|10|A1 to C1|
|11|A1 to C2|
|12|A1 to C3|
|13|A1 to D1|
|14|A1 to D4|
|15|A1 to E1|
|16|A1 to E5|
|17|A1 to F1|
|18|A1 to F6|
|19|A1 to G1|
|20|A1 to G7|
|21|A1 to H1|
|22|A1 to H8|
|23|A2 to A1|
|24|A2 to A3|
|25|A2 to A4|
|26|A2 to A5|
|27|A2 to A6|
|28|A2 to A7|
|29|A2 to A8|
|30|A2 to B1|
|31|A2 to B2|
|32|A2 to B3|
|33|A2 to B4|
|34|A2 to C1|
|35|A2 to C2|
|36|A2 to C3|
|37|A2 to C4|
|38|A2 to D2|
|39|A2 to D5|
|40|A2 to E2|
|41|A2 to E6|
|42|A2 to F2|
|43|A2 to F7|
|44|A2 to G2|
|45|A2 to G8|
|46|A2 to H2|
|47|A3 to A1|
|48|A3 to A2|
|49|A3 to A4|
|50|A3 to A5|
|51|A3 to A6|
|52|A3 to A7|
|53|A3 to A8|
|54|A3 to B1|
|55|A3 to B2|
|56|A3 to B3|
|57|A3 to B4|
|58|A3 to B5|
|59|A3 to C1|
|60|A3 to C2|
|61|A3 to C3|
|62|A3 to C4|
|63|A3 to C5|
|64|A3 to D3|
|65|A3 to D6|
|66|A3 to E3|
|67|A3 to E7|
|68|A3 to F3|
|69|A3 to F8|
|70|A3 to G3|
|71|A3 to H3|
|72|A4 to A1|
|73|A4 to A2|
|74|A4 to A3|
|75|A4 to A5|
|76|A4 to A6|
|77|A4 to A7|
|78|A4 to A8|
|79|A4 to B2|
|80|A4 to B3|
|81|A4 to B4|
|82|A4 to B5|
|83|A4 to B6|
|84|A4 to C2|
|85|A4 to C3|
|86|A4 to C4|
|87|A4 to C5|
|88|A4 to C6|
|89|A4 to D1|
|90|A4 to D4|
|91|A4 to D7|
|92|A4 to E4|
|93|A4 to E8|
|94|A4 to F4|
|95|A4 to G4|
|96|A4 to H4|
|97|A5 to A1|
|98|A5 to A2|
|99|A5 to A3|
|100|A5 to A4|
|101|A5 to A6|
|102|A5 to A7|
|103|A5 to A8|
|104|A5 to B3|
|105|A5 to B4|
|106|A5 to B5|
|107|A5 to B6|
|108|A5 to B7|
|109|A5 to C3|
|110|A5 to C4|
|111|A5 to C5|
|112|A5 to C6|
|113|A5 to C7|
|114|A5 to D2|
|115|A5 to D5|
|116|A5 to D8|
|117|A5 to E1|
|118|A5 to E5|
|119|A5 to F5|
|120|A5 to G5|
|121|A5 to H5|
|122|A6 to A1|
|123|A6 to A2|
|124|A6 to A3|
|125|A6 to A4|
|126|A6 to A5|
|127|A6 to A7|
|128|A6 to A8|
|129|A6 to B4|
|130|A6 to B5|
|131|A6 to B6|
|132|A6 to B7|
|133|A6 to B8|
|134|A6 to C4|
|135|A6 to C5|
|136|A6 to C6|
|137|A6 to C7|
|138|A6 to C8|
|139|A6 to D3|
|140|A6 to D6|
|141|A6 to E2|
|142|A6 to E6|
|143|A6 to F1|
|144|A6 to F6|
|145|A6 to G6|
|146|A6 to H6|
|147|A7 to A1|
|148|A7 to A2|
|149|A7 to A3|
|150|A7 to A4|
|151|A7 to A5|
|152|A7 to A6|
|153|A7 to A8|
|154|A7 to B5|
|155|A7 to B6|
|156|A7 to B7|
|157|A7 to B8|
|158|A7 to C5|
|159|A7 to C6|
|160|A7 to C7|
|161|A7 to C8|
|162|A7 to D4|
|163|A7 to D7|
|164|A7 to E3|
|165|A7 to E7|
|166|A7 to F2|
|167|A7 to F7|
|168|A7 to G1|
|169|A7 to G7|
|170|A7 to H7|
|171|A8 to A1|
|172|A8 to A2|
|173|A8 to A3|
|174|A8 to A4|
|175|A8 to A5|
|176|A8 to A6|
|177|A8 to A7|
|178|A8 to B6|
|179|A8 to B7|
|180|A8 to B8|
|181|A8 to C6|
|182|A8 to C7|
|183|A8 to C8|
|184|A8 to D5|
|185|A8 to D8|
|186|A8 to E4|
|187|A8 to E8|
|188|A8 to F3|
|189|A8 to F8|
|190|A8 to G2|
|191|A8 to G8|
|192|A8 to H1|
|193|A8 to H8|
|194|B1 to A1|
|195|B1 to A2|
|196|B1 to A3|
|197|B1 to B2|
|198|B1 to B3|
|199|B1 to B4|
|200|B1 to B5|
|201|B1 to B6|
|202|B1 to B7|
|203|B1 to B8|
|204|B1 to C1|
|205|B1 to C2|
|206|B1 to C3|
|207|B1 to D1|
|208|B1 to D2|
|209|B1 to D3|
|210|B1 to E1|
|211|B1 to E4|
|212|B1 to F1|
|213|B1 to F5|
|214|B1 to G1|
|215|B1 to G6|
|216|B1 to H1|
|217|B1 to H7|
|218|B2 to A1|
|219|B2 to A2|
|220|B2 to A3|
|221|B2 to A4|
|222|B2 to B1|
|223|B2 to B3|
|224|B2 to B4|
|225|B2 to B5|
|226|B2 to B6|
|227|B2 to B7|
|228|B2 to B8|
|229|B2 to C1|
|230|B2 to C2|
|231|B2 to C3|
|232|B2 to C4|
|233|B2 to D1|
|234|B2 to D2|
|235|B2 to D3|
|236|B2 to D4|
|237|B2 to E2|
|238|B2 to E5|
|239|B2 to F2|
|240|B2 to F6|
|241|B2 to G2|
|242|B2 to G7|
|243|B2 to H2|
|244|B2 to H8|
|245|B3 to A1|
|246|B3 to A2|
|247|B3 to A3|
|248|B3 to A4|
|249|B3 to A5|
|250|B3 to B1|
|251|B3 to B2|
|252|B3 to B4|
|253|B3 to B5|
|254|B3 to B6|
|255|B3 to B7|
|256|B3 to B8|
|257|B3 to C1|
|258|B3 to C2|
|259|B3 to C3|
|260|B3 to C4|
|261|B3 to C5|
|262|B3 to D1|
|263|B3 to D2|
|264|B3 to D3|
|265|B3 to D4|
|266|B3 to D5|
|267|B3 to E3|
|268|B3 to E6|
|269|B3 to F3|
|270|B3 to F7|
|271|B3 to G3|
|272|B3 to G8|
|273|B3 to H3|
|274|B4 to A2|
|275|B4 to A3|
|276|B4 to A4|
|277|B4 to A5|
|278|B4 to A6|
|279|B4 to B1|
|280|B4 to B2|
|281|B4 to B3|
|282|B4 to B5|
|283|B4 to B6|
|284|B4 to B7|
|285|B4 to B8|
|286|B4 to C2|
|287|B4 to C3|
|288|B4 to C4|
|289|B4 to C5|
|290|B4 to C6|
|291|B4 to D2|
|292|B4 to D3|
|293|B4 to D4|
|294|B4 to D5|
|295|B4 to D6|
|296|B4 to E1|
|297|B4 to E4|
|298|B4 to E7|
|299|B4 to F4|
|300|B4 to F8|
|301|B4 to G4|
|302|B4 to H4|
|303|B5 to A3|
|304|B5 to A4|
|305|B5 to A5|
|306|B5 to A6|
|307|B5 to A7|
|308|B5 to B1|
|309|B5 to B2|
|310|B5 to B3|
|311|B5 to B4|
|312|B5 to B6|
|313|B5 to B7|
|314|B5 to B8|
|315|B5 to C3|
|316|B5 to C4|
|317|B5 to C5|
|318|B5 to C6|
|319|B5 to C7|
|320|B5 to D3|
|321|B5 to D4|
|322|B5 to D5|
|323|B5 to D6|
|324|B5 to D7|
|325|B5 to E2|
|326|B5 to E5|
|327|B5 to E8|
|328|B5 to F1|
|329|B5 to F5|
|330|B5 to G5|
|331|B5 to H5|
|332|B6 to A4|
|333|B6 to A5|
|334|B6 to A6|
|335|B6 to A7|
|336|B6 to A8|
|337|B6 to B1|
|338|B6 to B2|
|339|B6 to B3|
|340|B6 to B4|
|341|B6 to B5|
|342|B6 to B7|
|343|B6 to B8|
|344|B6 to C4|
|345|B6 to C5|
|346|B6 to C6|
|347|B6 to C7|
|348|B6 to C8|
|349|B6 to D4|
|350|B6 to D5|
|351|B6 to D6|
|352|B6 to D7|
|353|B6 to D8|
|354|B6 to E3|
|355|B6 to E6|
|356|B6 to F2|
|357|B6 to F6|
|358|B6 to G1|
|359|B6 to G6|
|360|B6 to H6|
|361|B7 to A5|
|362|B7 to A6|
|363|B7 to A7|
|364|B7 to A8|
|365|B7 to B1|
|366|B7 to B2|
|367|B7 to B3|
|368|B7 to B4|
|369|B7 to B5|
|370|B7 to B6|
|371|B7 to B8|
|372|B7 to C5|
|373|B7 to C6|
|374|B7 to C7|
|375|B7 to C8|
|376|B7 to D5|
|377|B7 to D6|
|378|B7 to D7|
|379|B7 to D8|
|380|B7 to E4|
|381|B7 to E7|
|382|B7 to F3|
|383|B7 to F7|
|384|B7 to G2|
|385|B7 to G7|
|386|B7 to H1|
|387|B7 to H7|
|388|B8 to A6|
|389|B8 to A7|
|390|B8 to A8|
|391|B8 to B1|
|392|B8 to B2|
|393|B8 to B3|
|394|B8 to B4|
|395|B8 to B5|
|396|B8 to B6|
|397|B8 to B7|
|398|B8 to C6|
|399|B8 to C7|
|400|B8 to C8|
|401|B8 to D6|
|402|B8 to D7|
|403|B8 to D8|
|404|B8 to E5|
|405|B8 to E8|
|406|B8 to F4|
|407|B8 to F8|
|408|B8 to G3|
|409|B8 to G8|
|410|B8 to H2|
|411|B8 to H8|
|412|C1 to A1|
|413|C1 to A2|
|414|C1 to A3|
|415|C1 to B1|
|416|C1 to B2|
|417|C1 to B3|
|418|C1 to C2|
|419|C1 to C3|
|420|C1 to C4|
|421|C1 to C5|
|422|C1 to C6|
|423|C1 to C7|
|424|C1 to C8|
|425|C1 to D1|
|426|C1 to D2|
|427|C1 to D3|
|428|C1 to E1|
|429|C1 to E2|
|430|C1 to E3|
|431|C1 to F1|
|432|C1 to F4|
|433|C1 to G1|
|434|C1 to G5|
|435|C1 to H1|
|436|C1 to H6|
|437|C2 to A1|
|438|C2 to A2|
|439|C2 to A3|
|440|C2 to A4|
|441|C2 to B1|
|442|C2 to B2|
|443|C2 to B3|
|444|C2 to B4|
|445|C2 to C1|
|446|C2 to C3|
|447|C2 to C4|
|448|C2 to C5|
|449|C2 to C6|
|450|C2 to C7|
|451|C2 to C8|
|452|C2 to D1|
|453|C2 to D2|
|454|C2 to D3|
|455|C2 to D4|
|456|C2 to E1|
|457|C2 to E2|
|458|C2 to E3|
|459|C2 to E4|
|460|C2 to F2|
|461|C2 to F5|
|462|C2 to G2|
|463|C2 to G6|
|464|C2 to H2|
|465|C2 to H7|
|466|C3 to A1|
|467|C3 to A2|
|468|C3 to A3|
|469|C3 to A4|
|470|C3 to A5|
|471|C3 to B1|
|472|C3 to B2|
|473|C3 to B3|
|474|C3 to B4|
|475|C3 to B5|
|476|C3 to C1|
|477|C3 to C2|
|478|C3 to C4|
|479|C3 to C5|
|480|C3 to C6|
|481|C3 to C7|
|482|C3 to C8|
|483|C3 to D1|
|484|C3 to D2|
|485|C3 to D3|
|486|C3 to D4|
|487|C3 to D5|
|488|C3 to E1|
|489|C3 to E2|
|490|C3 to E3|
|491|C3 to E4|
|492|C3 to E5|
|493|C3 to F3|
|494|C3 to F6|
|495|C3 to G3|
|496|C3 to G7|
|497|C3 to H3|
|498|C3 to H8|
|499|C4 to A2|
|500|C4 to A3|
|501|C4 to A4|
|502|C4 to A5|
|503|C4 to A6|
|504|C4 to B2|
|505|C4 to B3|
|506|C4 to B4|
|507|C4 to B5|
|508|C4 to B6|
|509|C4 to C1|
|510|C4 to C2|
|511|C4 to C3|
|512|C4 to C5|
|513|C4 to C6|
|514|C4 to C7|
|515|C4 to C8|
|516|C4 to D2|
|517|C4 to D3|
|518|C4 to D4|
|519|C4 to D5|
|520|C4 to D6|
|521|C4 to E2|
|522|C4 to E3|
|523|C4 to E4|
|524|C4 to E5|
|525|C4 to E6|
|526|C4 to F1|
|527|C4 to F4|
|528|C4 to F7|
|529|C4 to G4|
|530|C4 to G8|
|531|C4 to H4|
|532|C5 to A3|
|533|C5 to A4|
|534|C5 to A5|
|535|C5 to A6|
|536|C5 to A7|
|537|C5 to B3|
|538|C5 to B4|
|539|C5 to B5|
|540|C5 to B6|
|541|C5 to B7|
|542|C5 to C1|
|543|C5 to C2|
|544|C5 to C3|
|545|C5 to C4|
|546|C5 to C6|
|547|C5 to C7|
|548|C5 to C8|
|549|C5 to D3|
|550|C5 to D4|
|551|C5 to D5|
|552|C5 to D6|
|553|C5 to D7|
|554|C5 to E3|
|555|C5 to E4|
|556|C5 to E5|
|557|C5 to E6|
|558|C5 to E7|
|559|C5 to F2|
|560|C5 to F5|
|561|C5 to F8|
|562|C5 to G1|
|563|C5 to G5|
|564|C5 to H5|
|565|C6 to A4|
|566|C6 to A5|
|567|C6 to A6|
|568|C6 to A7|
|569|C6 to A8|
|570|C6 to B4|
|571|C6 to B5|
|572|C6 to B6|
|573|C6 to B7|
|574|C6 to B8|
|575|C6 to C1|
|576|C6 to C2|
|577|C6 to C3|
|578|C6 to C4|
|579|C6 to C5|
|580|C6 to C7|
|581|C6 to C8|
|582|C6 to D4|
|583|C6 to D5|
|584|C6 to D6|
|585|C6 to D7|
|586|C6 to D8|
|587|C6 to E4|
|588|C6 to E5|
|589|C6 to E6|
|590|C6 to E7|
|591|C6 to E8|
|592|C6 to F3|
|593|C6 to F6|
|594|C6 to G2|
|595|C6 to G6|
|596|C6 to H1|
|597|C6 to H6|
|598|C7 to A5|
|599|C7 to A6|
|600|C7 to A7|
|601|C7 to A8|
|602|C7 to B5|
|603|C7 to B6|
|604|C7 to B7|
|605|C7 to B8|
|606|C7 to C1|
|607|C7 to C2|
|608|C7 to C3|
|609|C7 to C4|
|610|C7 to C5|
|611|C7 to C6|
|612|C7 to C8|
|613|C7 to D5|
|614|C7 to D6|
|615|C7 to D7|
|616|C7 to D8|
|617|C7 to E5|
|618|C7 to E6|
|619|C7 to E7|
|620|C7 to E8|
|621|C7 to F4|
|622|C7 to F7|
|623|C7 to G3|
|624|C7 to G7|
|625|C7 to H2|
|626|C7 to H7|
|627|C8 to A6|
|628|C8 to A7|
|629|C8 to A8|
|630|C8 to B6|
|631|C8 to B7|
|632|C8 to B8|
|633|C8 to C1|
|634|C8 to C2|
|635|C8 to C3|
|636|C8 to C4|
|637|C8 to C5|
|638|C8 to C6|
|639|C8 to C7|
|640|C8 to D6|
|641|C8 to D7|
|642|C8 to D8|
|643|C8 to E6|
|644|C8 to E7|
|645|C8 to E8|
|646|C8 to F5|
|647|C8 to F8|
|648|C8 to G4|
|649|C8 to G8|
|650|C8 to H3|
|651|C8 to H8|
|652|D1 to A1|
|653|D1 to A4|
|654|D1 to B1|
|655|D1 to B2|
|656|D1 to B3|
|657|D1 to C1|
|658|D1 to C2|
|659|D1 to C3|
|660|D1 to D2|
|661|D1 to D3|
|662|D1 to D4|
|663|D1 to D5|
|664|D1 to D6|
|665|D1 to D7|
|666|D1 to D8|
|667|D1 to E1|
|668|D1 to E2|
|669|D1 to E3|
|670|D1 to F1|
|671|D1 to F2|
|672|D1 to F3|
|673|D1 to G1|
|674|D1 to G4|
|675|D1 to H1|
|676|D1 to H5|
|677|D2 to A2|
|678|D2 to A5|
|679|D2 to B1|
|680|D2 to B2|
|681|D2 to B3|
|682|D2 to B4|
|683|D2 to C1|
|684|D2 to C2|
|685|D2 to C3|
|686|D2 to C4|
|687|D2 to D1|
|688|D2 to D3|
|689|D2 to D4|
|690|D2 to D5|
|691|D2 to D6|
|692|D2 to D7|
|693|D2 to D8|
|694|D2 to E1|
|695|D2 to E2|
|696|D2 to E3|
|697|D2 to E4|
|698|D2 to F1|
|699|D2 to F2|
|700|D2 to F3|
|701|D2 to F4|
|702|D2 to G2|
|703|D2 to G5|
|704|D2 to H2|
|705|D2 to H6|
|706|D3 to A3|
|707|D3 to A6|
|708|D3 to B1|
|709|D3 to B2|
|710|D3 to B3|
|711|D3 to B4|
|712|D3 to B5|
|713|D3 to C1|
|714|D3 to C2|
|715|D3 to C3|
|716|D3 to C4|
|717|D3 to C5|
|718|D3 to D1|
|719|D3 to D2|
|720|D3 to D4|
|721|D3 to D5|
|722|D3 to D6|
|723|D3 to D7|
|724|D3 to D8|
|725|D3 to E1|
|726|D3 to E2|
|727|D3 to E3|
|728|D3 to E4|
|729|D3 to E5|
|730|D3 to F1|
|731|D3 to F2|
|732|D3 to F3|
|733|D3 to F4|
|734|D3 to F5|
|735|D3 to G3|
|736|D3 to G6|
|737|D3 to H3|
|738|D3 to H7|
|739|D4 to A1|
|740|D4 to A4|
|741|D4 to A7|
|742|D4 to B2|
|743|D4 to B3|
|744|D4 to B4|
|745|D4 to B5|
|746|D4 to B6|
|747|D4 to C2|
|748|D4 to C3|
|749|D4 to C4|
|750|D4 to C5|
|751|D4 to C6|
|752|D4 to D1|
|753|D4 to D2|
|754|D4 to D3|
|755|D4 to D5|
|756|D4 to D6|
|757|D4 to D7|
|758|D4 to D8|
|759|D4 to E2|
|760|D4 to E3|
|761|D4 to E4|
|762|D4 to E5|
|763|D4 to E6|
|764|D4 to F2|
|765|D4 to F3|
|766|D4 to F4|
|767|D4 to F5|
|768|D4 to F6|
|769|D4 to G1|
|770|D4 to G4|
|771|D4 to G7|
|772|D4 to H4|
|773|D4 to H8|
|774|D5 to A2|
|775|D5 to A5|
|776|D5 to A8|
|777|D5 to B3|
|778|D5 to B4|
|779|D5 to B5|
|780|D5 to B6|
|781|D5 to B7|
|782|D5 to C3|
|783|D5 to C4|
|784|D5 to C5|
|785|D5 to C6|
|786|D5 to C7|
|787|D5 to D1|
|788|D5 to D2|
|789|D5 to D3|
|790|D5 to D4|
|791|D5 to D6|
|792|D5 to D7|
|793|D5 to D8|
|794|D5 to E3|
|795|D5 to E4|
|796|D5 to E5|
|797|D5 to E6|
|798|D5 to E7|
|799|D5 to F3|
|800|D5 to F4|
|801|D5 to F5|
|802|D5 to F6|
|803|D5 to F7|
|804|D5 to G2|
|805|D5 to G5|
|806|D5 to G8|
|807|D5 to H1|
|808|D5 to H5|
|809|D6 to A3|
|810|D6 to A6|
|811|D6 to B4|
|812|D6 to B5|
|813|D6 to B6|
|814|D6 to B7|
|815|D6 to B8|
|816|D6 to C4|
|817|D6 to C5|
|818|D6 to C6|
|819|D6 to C7|
|820|D6 to C8|
|821|D6 to D1|
|822|D6 to D2|
|823|D6 to D3|
|824|D6 to D4|
|825|D6 to D5|
|826|D6 to D7|
|827|D6 to D8|
|828|D6 to E4|
|829|D6 to E5|
|830|D6 to E6|
|831|D6 to E7|
|832|D6 to E8|
|833|D6 to F4|
|834|D6 to F5|
|835|D6 to F6|
|836|D6 to F7|
|837|D6 to F8|
|838|D6 to G3|
|839|D6 to G6|
|840|D6 to H2|
|841|D6 to H6|
|842|D7 to A4|
|843|D7 to A7|
|844|D7 to B5|
|845|D7 to B6|
|846|D7 to B7|
|847|D7 to B8|
|848|D7 to C5|
|849|D7 to C6|
|850|D7 to C7|
|851|D7 to C8|
|852|D7 to D1|
|853|D7 to D2|
|854|D7 to D3|
|855|D7 to D4|
|856|D7 to D5|
|857|D7 to D6|
|858|D7 to D8|
|859|D7 to E5|
|860|D7 to E6|
|861|D7 to E7|
|862|D7 to E8|
|863|D7 to F5|
|864|D7 to F6|
|865|D7 to F7|
|866|D7 to F8|
|867|D7 to G4|
|868|D7 to G7|
|869|D7 to H3|
|870|D7 to H7|
|871|D8 to A5|
|872|D8 to A8|
|873|D8 to B6|
|874|D8 to B7|
|875|D8 to B8|
|876|D8 to C6|
|877|D8 to C7|
|878|D8 to C8|
|879|D8 to D1|
|880|D8 to D2|
|881|D8 to D3|
|882|D8 to D4|
|883|D8 to D5|
|884|D8 to D6|
|885|D8 to D7|
|886|D8 to E6|
|887|D8 to E7|
|888|D8 to E8|
|889|D8 to F6|
|890|D8 to F7|
|891|D8 to F8|
|892|D8 to G5|
|893|D8 to G8|
|894|D8 to H4|
|895|D8 to H8|
|896|E1 to A1|
|897|E1 to A5|
|898|E1 to B1|
|899|E1 to B4|
|900|E1 to C1|
|901|E1 to C2|
|902|E1 to C3|
|903|E1 to D1|
|904|E1 to D2|
|905|E1 to D3|
|906|E1 to E2|
|907|E1 to E3|
|908|E1 to E4|
|909|E1 to E5|
|910|E1 to E6|
|911|E1 to E7|
|912|E1 to E8|
|913|E1 to F1|
|914|E1 to F2|
|915|E1 to F3|
|916|E1 to G1|
|917|E1 to G2|
|918|E1 to G3|
|919|E1 to H1|
|920|E1 to H4|
|921|E2 to A2|
|922|E2 to A6|
|923|E2 to B2|
|924|E2 to B5|
|925|E2 to C1|
|926|E2 to C2|
|927|E2 to C3|
|928|E2 to C4|
|929|E2 to D1|
|930|E2 to D2|
|931|E2 to D3|
|932|E2 to D4|
|933|E2 to E1|
|934|E2 to E3|
|935|E2 to E4|
|936|E2 to E5|
|937|E2 to E6|
|938|E2 to E7|
|939|E2 to E8|
|940|E2 to F1|
|941|E2 to F2|
|942|E2 to F3|
|943|E2 to F4|
|944|E2 to G1|
|945|E2 to G2|
|946|E2 to G3|
|947|E2 to G4|
|948|E2 to H2|
|949|E2 to H5|
|950|E3 to A3|
|951|E3 to A7|
|952|E3 to B3|
|953|E3 to B6|
|954|E3 to C1|
|955|E3 to C2|
|956|E3 to C3|
|957|E3 to C4|
|958|E3 to C5|
|959|E3 to D1|
|960|E3 to D2|
|961|E3 to D3|
|962|E3 to D4|
|963|E3 to D5|
|964|E3 to E1|
|965|E3 to E2|
|966|E3 to E4|
|967|E3 to E5|
|968|E3 to E6|
|969|E3 to E7|
|970|E3 to E8|
|971|E3 to F1|
|972|E3 to F2|
|973|E3 to F3|
|974|E3 to F4|
|975|E3 to F5|
|976|E3 to G1|
|977|E3 to G2|
|978|E3 to G3|
|979|E3 to G4|
|980|E3 to G5|
|981|E3 to H3|
|982|E3 to H6|
|983|E4 to A4|
|984|E4 to A8|
|985|E4 to B1|
|986|E4 to B4|
|987|E4 to B7|
|988|E4 to C2|
|989|E4 to C3|
|990|E4 to C4|
|991|E4 to C5|
|992|E4 to C6|
|993|E4 to D2|
|994|E4 to D3|
|995|E4 to D4|
|996|E4 to D5|
|997|E4 to D6|
|998|E4 to E1|
|999|E4 to E2|
|1,000|E4 to E3|
|1,001|E4 to E5|
|1,002|E4 to E6|
|1,003|E4 to E7|
|1,004|E4 to E8|
|1,005|E4 to F2|
|1,006|E4 to F3|
|1,007|E4 to F4|
|1,008|E4 to F5|
|1,009|E4 to F6|
|1,010|E4 to G2|
|1,011|E4 to G3|
|1,012|E4 to G4|
|1,013|E4 to G5|
|1,014|E4 to G6|
|1,015|E4 to H1|
|1,016|E4 to H4|
|1,017|E4 to H7|
|1,018|E5 to A1|
|1,019|E5 to A5|
|1,020|E5 to B2|
|1,021|E5 to B5|
|1,022|E5 to B8|
|1,023|E5 to C3|
|1,024|E5 to C4|
|1,025|E5 to C5|
|1,026|E5 to C6|
|1,027|E5 to C7|
|1,028|E5 to D3|
|1,029|E5 to D4|
|1,030|E5 to D5|
|1,031|E5 to D6|
|1,032|E5 to D7|
|1,033|E5 to E1|
|1,034|E5 to E2|
|1,035|E5 to E3|
|1,036|E5 to E4|
|1,037|E5 to E6|
|1,038|E5 to E7|
|1,039|E5 to E8|
|1,040|E5 to F3|
|1,041|E5 to F4|
|1,042|E5 to F5|
|1,043|E5 to F6|
|1,044|E5 to F7|
|1,045|E5 to G3|
|1,046|E5 to G4|
|1,047|E5 to G5|
|1,048|E5 to G6|
|1,049|E5 to G7|
|1,050|E5 to H2|
|1,051|E5 to H5|
|1,052|E5 to H8|
|1,053|E6 to A2|
|1,054|E6 to A6|
|1,055|E6 to B3|
|1,056|E6 to B6|
|1,057|E6 to C4|
|1,058|E6 to C5|
|1,059|E6 to C6|
|1,060|E6 to C7|
|1,061|E6 to C8|
|1,062|E6 to D4|
|1,063|E6 to D5|
|1,064|E6 to D6|
|1,065|E6 to D7|
|1,066|E6 to D8|
|1,067|E6 to E1|
|1,068|E6 to E2|
|1,069|E6 to E3|
|1,070|E6 to E4|
|1,071|E6 to E5|
|1,072|E6 to E7|
|1,073|E6 to E8|
|1,074|E6 to F4|
|1,075|E6 to F5|
|1,076|E6 to F6|
|1,077|E6 to F7|
|1,078|E6 to F8|
|1,079|E6 to G4|
|1,080|E6 to G5|
|1,081|E6 to G6|
|1,082|E6 to G7|
|1,083|E6 to G8|
|1,084|E6 to H3|
|1,085|E6 to H6|
|1,086|E7 to A3|
|1,087|E7 to A7|
|1,088|E7 to B4|
|1,089|E7 to B7|
|1,090|E7 to C5|
|1,091|E7 to C6|
|1,092|E7 to C7|
|1,093|E7 to C8|
|1,094|E7 to D5|
|1,095|E7 to D6|
|1,096|E7 to D7|
|1,097|E7 to D8|
|1,098|E7 to E1|
|1,099|E7 to E2|
|1,100|E7 to E3|
|1,101|E7 to E4|
|1,102|E7 to E5|
|1,103|E7 to E6|
|1,104|E7 to E8|
|1,105|E7 to F5|
|1,106|E7 to F6|
|1,107|E7 to F7|
|1,108|E7 to F8|
|1,109|E7 to G5|
|1,110|E7 to G6|
|1,111|E7 to G7|
|1,112|E7 to G8|
|1,113|E7 to H4|
|1,114|E7 to H7|
|1,115|E8 to A4|
|1,116|E8 to A8|
|1,117|E8 to B5|
|1,118|E8 to B8|
|1,119|E8 to C6|
|1,120|E8 to C7|
|1,121|E8 to C8|
|1,122|E8 to D6|
|1,123|E8 to D7|
|1,124|E8 to D8|
|1,125|E8 to E1|
|1,126|E8 to E2|
|1,127|E8 to E3|
|1,128|E8 to E4|
|1,129|E8 to E5|
|1,130|E8 to E6|
|1,131|E8 to E7|
|1,132|E8 to F6|
|1,133|E8 to F7|
|1,134|E8 to F8|
|1,135|E8 to G6|
|1,136|E8 to G7|
|1,137|E8 to G8|
|1,138|E8 to H5|
|1,139|E8 to H8|
|1,140|F1 to A1|
|1,141|F1 to A6|
|1,142|F1 to B1|
|1,143|F1 to B5|
|1,144|F1 to C1|
|1,145|F1 to C4|
|1,146|F1 to D1|
|1,147|F1 to D2|
|1,148|F1 to D3|
|1,149|F1 to E1|
|1,150|F1 to E2|
|1,151|F1 to E3|
|1,152|F1 to F2|
|1,153|F1 to F3|
|1,154|F1 to F4|
|1,155|F1 to F5|
|1,156|F1 to F6|
|1,157|F1 to F7|
|1,158|F1 to F8|
|1,159|F1 to G1|
|1,160|F1 to G2|
|1,161|F1 to G3|
|1,162|F1 to H1|
|1,163|F1 to H2|
|1,164|F1 to H3|
|1,165|F2 to A2|
|1,166|F2 to A7|
|1,167|F2 to B2|
|1,168|F2 to B6|
|1,169|F2 to C2|
|1,170|F2 to C5|
|1,171|F2 to D1|
|1,172|F2 to D2|
|1,173|F2 to D3|
|1,174|F2 to D4|
|1,175|F2 to E1|
|1,176|F2 to E2|
|1,177|F2 to E3|
|1,178|F2 to E4|
|1,179|F2 to F1|
|1,180|F2 to F3|
|1,181|F2 to F4|
|1,182|F2 to F5|
|1,183|F2 to F6|
|1,184|F2 to F7|
|1,185|F2 to F8|
|1,186|F2 to G1|
|1,187|F2 to G2|
|1,188|F2 to G3|
|1,189|F2 to G4|
|1,190|F2 to H1|
|1,191|F2 to H2|
|1,192|F2 to H3|
|1,193|F2 to H4|
|1,194|F3 to A3|
|1,195|F3 to A8|
|1,196|F3 to B3|
|1,197|F3 to B7|
|1,198|F3 to C3|
|1,199|F3 to C6|
|1,200|F3 to D1|
|1,201|F3 to D2|
|1,202|F3 to D3|
|1,203|F3 to D4|
|1,204|F3 to D5|
|1,205|F3 to E1|
|1,206|F3 to E2|
|1,207|F3 to E3|
|1,208|F3 to E4|
|1,209|F3 to E5|
|1,210|F3 to F1|
|1,211|F3 to F2|
|1,212|F3 to F4|
|1,213|F3 to F5|
|1,214|F3 to F6|
|1,215|F3 to F7|
|1,216|F3 to F8|
|1,217|F3 to G1|
|1,218|F3 to G2|
|1,219|F3 to G3|
|1,220|F3 to G4|
|1,221|F3 to G5|
|1,222|F3 to H1|
|1,223|F3 to H2|
|1,224|F3 to H3|
|1,225|F3 to H4|
|1,226|F3 to H5|
|1,227|F4 to A4|
|1,228|F4 to B4|
|1,229|F4 to B8|
|1,230|F4 to C1|
|1,231|F4 to C4|
|1,232|F4 to C7|
|1,233|F4 to D2|
|1,234|F4 to D3|
|1,235|F4 to D4|
|1,236|F4 to D5|
|1,237|F4 to D6|
|1,238|F4 to E2|
|1,239|F4 to E3|
|1,240|F4 to E4|
|1,241|F4 to E5|
|1,242|F4 to E6|
|1,243|F4 to F1|
|1,244|F4 to F2|
|1,245|F4 to F3|
|1,246|F4 to F5|
|1,247|F4 to F6|
|1,248|F4 to F7|
|1,249|F4 to F8|
|1,250|F4 to G2|
|1,251|F4 to G3|
|1,252|F4 to G4|
|1,253|F4 to G5|
|1,254|F4 to G6|
|1,255|F4 to H2|
|1,256|F4 to H3|
|1,257|F4 to H4|
|1,258|F4 to H5|
|1,259|F4 to H6|
|1,260|F5 to A5|
|1,261|F5 to B1|
|1,262|F5 to B5|
|1,263|F5 to C2|
|1,264|F5 to C5|
|1,265|F5 to C8|
|1,266|F5 to D3|
|1,267|F5 to D4|
|1,268|F5 to D5|
|1,269|F5 to D6|
|1,270|F5 to D7|
|1,271|F5 to E3|
|1,272|F5 to E4|
|1,273|F5 to E5|
|1,274|F5 to E6|
|1,275|F5 to E7|
|1,276|F5 to F1|
|1,277|F5 to F2|
|1,278|F5 to F3|
|1,279|F5 to F4|
|1,280|F5 to F6|
|1,281|F5 to F7|
|1,282|F5 to F8|
|1,283|F5 to G3|
|1,284|F5 to G4|
|1,285|F5 to G5|
|1,286|F5 to G6|
|1,287|F5 to G7|
|1,288|F5 to H3|
|1,289|F5 to H4|
|1,290|F5 to H5|
|1,291|F5 to H6|
|1,292|F5 to H7|
|1,293|F6 to A1|
|1,294|F6 to A6|
|1,295|F6 to B2|
|1,296|F6 to B6|
|1,297|F6 to C3|
|1,298|F6 to C6|
|1,299|F6 to D4|
|1,300|F6 to D5|
|1,301|F6 to D6|
|1,302|F6 to D7|
|1,303|F6 to D8|
|1,304|F6 to E4|
|1,305|F6 to E5|
|1,306|F6 to E6|
|1,307|F6 to E7|
|1,308|F6 to E8|
|1,309|F6 to F1|
|1,310|F6 to F2|
|1,311|F6 to F3|
|1,312|F6 to F4|
|1,313|F6 to F5|
|1,314|F6 to F7|
|1,315|F6 to F8|
|1,316|F6 to G4|
|1,317|F6 to G5|
|1,318|F6 to G6|
|1,319|F6 to G7|
|1,320|F6 to G8|
|1,321|F6 to H4|
|1,322|F6 to H5|
|1,323|F6 to H6|
|1,324|F6 to H7|
|1,325|F6 to H8|
|1,326|F7 to A2|
|1,327|F7 to A7|
|1,328|F7 to B3|
|1,329|F7 to B7|
|1,330|F7 to C4|
|1,331|F7 to C7|
|1,332|F7 to D5|
|1,333|F7 to D6|
|1,334|F7 to D7|
|1,335|F7 to D8|
|1,336|F7 to E5|
|1,337|F7 to E6|
|1,338|F7 to E7|
|1,339|F7 to E8|
|1,340|F7 to F1|
|1,341|F7 to F2|
|1,342|F7 to F3|
|1,343|F7 to F4|
|1,344|F7 to F5|
|1,345|F7 to F6|
|1,346|F7 to F8|
|1,347|F7 to G5|
|1,348|F7 to G6|
|1,349|F7 to G7|
|1,350|F7 to G8|
|1,351|F7 to H5|
|1,352|F7 to H6|
|1,353|F7 to H7|
|1,354|F7 to H8|
|1,355|F8 to A3|
|1,356|F8 to A8|
|1,357|F8 to B4|
|1,358|F8 to B8|
|1,359|F8 to C5|
|1,360|F8 to C8|
|1,361|F8 to D6|
|1,362|F8 to D7|
|1,363|F8 to D8|
|1,364|F8 to E6|
|1,365|F8 to E7|
|1,366|F8 to E8|
|1,367|F8 to F1|
|1,368|F8 to F2|
|1,369|F8 to F3|
|1,370|F8 to F4|
|1,371|F8 to F5|
|1,372|F8 to F6|
|1,373|F8 to F7|
|1,374|F8 to G6|
|1,375|F8 to G7|
|1,376|F8 to G8|
|1,377|F8 to H6|
|1,378|F8 to H7|
|1,379|F8 to H8|
|1,380|G1 to A1|
|1,381|G1 to A7|
|1,382|G1 to B1|
|1,383|G1 to B6|
|1,384|G1 to C1|
|1,385|G1 to C5|
|1,386|G1 to D1|
|1,387|G1 to D4|
|1,388|G1 to E1|
|1,389|G1 to E2|
|1,390|G1 to E3|
|1,391|G1 to F1|
|1,392|G1 to F2|
|1,393|G1 to F3|
|1,394|G1 to G2|
|1,395|G1 to G3|
|1,396|G1 to G4|
|1,397|G1 to G5|
|1,398|G1 to G6|
|1,399|G1 to G7|
|1,400|G1 to G8|
|1,401|G1 to H1|
|1,402|G1 to H2|
|1,403|G1 to H3|
|1,404|G2 to A2|
|1,405|G2 to A8|
|1,406|G2 to B2|
|1,407|G2 to B7|
|1,408|G2 to C2|
|1,409|G2 to C6|
|1,410|G2 to D2|
|1,411|G2 to D5|
|1,412|G2 to E1|
|1,413|G2 to E2|
|1,414|G2 to E3|
|1,415|G2 to E4|
|1,416|G2 to F1|
|1,417|G2 to F2|
|1,418|G2 to F3|
|1,419|G2 to F4|
|1,420|G2 to G1|
|1,421|G2 to G3|
|1,422|G2 to G4|
|1,423|G2 to G5|
|1,424|G2 to G6|
|1,425|G2 to G7|
|1,426|G2 to G8|
|1,427|G2 to H1|
|1,428|G2 to H2|
|1,429|G2 to H3|
|1,430|G2 to H4|
|1,431|G3 to A3|
|1,432|G3 to B3|
|1,433|G3 to B8|
|1,434|G3 to C3|
|1,435|G3 to C7|
|1,436|G3 to D3|
|1,437|G3 to D6|
|1,438|G3 to E1|
|1,439|G3 to E2|
|1,440|G3 to E3|
|1,441|G3 to E4|
|1,442|G3 to E5|
|1,443|G3 to F1|
|1,444|G3 to F2|
|1,445|G3 to F3|
|1,446|G3 to F4|
|1,447|G3 to F5|
|1,448|G3 to G1|
|1,449|G3 to G2|
|1,450|G3 to G4|
|1,451|G3 to G5|
|1,452|G3 to G6|
|1,453|G3 to G7|
|1,454|G3 to G8|
|1,455|G3 to H1|
|1,456|G3 to H2|
|1,457|G3 to H3|
|1,458|G3 to H4|
|1,459|G3 to H5|
|1,460|G4 to A4|
|1,461|G4 to B4|
|1,462|G4 to C4|
|1,463|G4 to C8|
|1,464|G4 to D1|
|1,465|G4 to D4|
|1,466|G4 to D7|
|1,467|G4 to E2|
|1,468|G4 to E3|
|1,469|G4 to E4|
|1,470|G4 to E5|
|1,471|G4 to E6|
|1,472|G4 to F2|
|1,473|G4 to F3|
|1,474|G4 to F4|
|1,475|G4 to F5|
|1,476|G4 to F6|
|1,477|G4 to G1|
|1,478|G4 to G2|
|1,479|G4 to G3|
|1,480|G4 to G5|
|1,481|G4 to G6|
|1,482|G4 to G7|
|1,483|G4 to G8|
|1,484|G4 to H2|
|1,485|G4 to H3|
|1,486|G4 to H4|
|1,487|G4 to H5|
|1,488|G4 to H6|
|1,489|G5 to A5|
|1,490|G5 to B5|
|1,491|G5 to C1|
|1,492|G5 to C5|
|1,493|G5 to D2|
|1,494|G5 to D5|
|1,495|G5 to D8|
|1,496|G5 to E3|
|1,497|G5 to E4|
|1,498|G5 to E5|
|1,499|G5 to E6|
|1,500|G5 to E7|
|1,501|G5 to F3|
|1,502|G5 to F4|
|1,503|G5 to F5|
|1,504|G5 to F6|
|1,505|G5 to F7|
|1,506|G5 to G1|
|1,507|G5 to G2|
|1,508|G5 to G3|
|1,509|G5 to G4|
|1,510|G5 to G6|
|1,511|G5 to G7|
|1,512|G5 to G8|
|1,513|G5 to H3|
|1,514|G5 to H4|
|1,515|G5 to H5|
|1,516|G5 to H6|
|1,517|G5 to H7|
|1,518|G6 to A6|
|1,519|G6 to B1|
|1,520|G6 to B6|
|1,521|G6 to C2|
|1,522|G6 to C6|
|1,523|G6 to D3|
|1,524|G6 to D6|
|1,525|G6 to E4|
|1,526|G6 to E5|
|1,527|G6 to E6|
|1,528|G6 to E7|
|1,529|G6 to E8|
|1,530|G6 to F4|
|1,531|G6 to F5|
|1,532|G6 to F6|
|1,533|G6 to F7|
|1,534|G6 to F8|
|1,535|G6 to G1|
|1,536|G6 to G2|
|1,537|G6 to G3|
|1,538|G6 to G4|
|1,539|G6 to G5|
|1,540|G6 to G7|
|1,541|G6 to G8|
|1,542|G6 to H4|
|1,543|G6 to H5|
|1,544|G6 to H6|
|1,545|G6 to H7|
|1,546|G6 to H8|
|1,547|G7 to A1|
|1,548|G7 to A7|
|1,549|G7 to B2|
|1,550|G7 to B7|
|1,551|G7 to C3|
|1,552|G7 to C7|
|1,553|G7 to D4|
|1,554|G7 to D7|
|1,555|G7 to E5|
|1,556|G7 to E6|
|1,557|G7 to E7|
|1,558|G7 to E8|
|1,559|G7 to F5|
|1,560|G7 to F6|
|1,561|G7 to F7|
|1,562|G7 to F8|
|1,563|G7 to G1|
|1,564|G7 to G2|
|1,565|G7 to G3|
|1,566|G7 to G4|
|1,567|G7 to G5|
|1,568|G7 to G6|
|1,569|G7 to G8|
|1,570|G7 to H5|
|1,571|G7 to H6|
|1,572|G7 to H7|
|1,573|G7 to H8|
|1,574|G8 to A2|
|1,575|G8 to A8|
|1,576|G8 to B3|
|1,577|G8 to B8|
|1,578|G8 to C4|
|1,579|G8 to C8|
|1,580|G8 to D5|
|1,581|G8 to D8|
|1,582|G8 to E6|
|1,583|G8 to E7|
|1,584|G8 to E8|
|1,585|G8 to F6|
|1,586|G8 to F7|
|1,587|G8 to F8|
|1,588|G8 to G1|
|1,589|G8 to G2|
|1,590|G8 to G3|
|1,591|G8 to G4|
|1,592|G8 to G5|
|1,593|G8 to G6|
|1,594|G8 to G7|
|1,595|G8 to H6|
|1,596|G8 to H7|
|1,597|G8 to H8|
|1,598|H1 to A1|
|1,599|H1 to A8|
|1,600|H1 to B1|
|1,601|H1 to B7|
|1,602|H1 to C1|
|1,603|H1 to C6|
|1,604|H1 to D1|
|1,605|H1 to D5|
|1,606|H1 to E1|
|1,607|H1 to E4|
|1,608|H1 to F1|
|1,609|H1 to F2|
|1,610|H1 to F3|
|1,611|H1 to G1|
|1,612|H1 to G2|
|1,613|H1 to G3|
|1,614|H1 to H2|
|1,615|H1 to H3|
|1,616|H1 to H4|
|1,617|H1 to H5|
|1,618|H1 to H6|
|1,619|H1 to H7|
|1,620|H1 to H8|
|1,621|H2 to A2|
|1,622|H2 to B2|
|1,623|H2 to B8|
|1,624|H2 to C2|
|1,625|H2 to C7|
|1,626|H2 to D2|
|1,627|H2 to D6|
|1,628|H2 to E2|
|1,629|H2 to E5|
|1,630|H2 to F1|
|1,631|H2 to F2|
|1,632|H2 to F3|
|1,633|H2 to F4|
|1,634|H2 to G1|
|1,635|H2 to G2|
|1,636|H2 to G3|
|1,637|H2 to G4|
|1,638|H2 to H1|
|1,639|H2 to H3|
|1,640|H2 to H4|
|1,641|H2 to H5|
|1,642|H2 to H6|
|1,643|H2 to H7|
|1,644|H2 to H8|
|1,645|H3 to A3|
|1,646|H3 to B3|
|1,647|H3 to C3|
|1,648|H3 to C8|
|1,649|H3 to D3|
|1,650|H3 to D7|
|1,651|H3 to E3|
|1,652|H3 to E6|
|1,653|H3 to F1|
|1,654|H3 to F2|
|1,655|H3 to F3|
|1,656|H3 to F4|
|1,657|H3 to F5|
|1,658|H3 to G1|
|1,659|H3 to G2|
|1,660|H3 to G3|
|1,661|H3 to G4|
|1,662|H3 to G5|
|1,663|H3 to H1|
|1,664|H3 to H2|
|1,665|H3 to H4|
|1,666|H3 to H5|
|1,667|H3 to H6|
|1,668|H3 to H7|
|1,669|H3 to H8|
|1,670|H4 to A4|
|1,671|H4 to B4|
|1,672|H4 to C4|
|1,673|H4 to D4|
|1,674|H4 to D8|
|1,675|H4 to E1|
|1,676|H4 to E4|
|1,677|H4 to E7|
|1,678|H4 to F2|
|1,679|H4 to F3|
|1,680|H4 to F4|
|1,681|H4 to F5|
|1,682|H4 to F6|
|1,683|H4 to G2|
|1,684|H4 to G3|
|1,685|H4 to G4|
|1,686|H4 to G5|
|1,687|H4 to G6|
|1,688|H4 to H1|
|1,689|H4 to H2|
|1,690|H4 to H3|
|1,691|H4 to H5|
|1,692|H4 to H6|
|1,693|H4 to H7|
|1,694|H4 to H8|
|1,695|H5 to A5|
|1,696|H5 to B5|
|1,697|H5 to C5|
|1,698|H5 to D1|
|1,699|H5 to D5|
|1,700|H5 to E2|
|1,701|H5 to E5|
|1,702|H5 to E8|
|1,703|H5 to F3|
|1,704|H5 to F4|
|1,705|H5 to F5|
|1,706|H5 to F6|
|1,707|H5 to F7|
|1,708|H5 to G3|
|1,709|H5 to G4|
|1,710|H5 to G5|
|1,711|H5 to G6|
|1,712|H5 to G7|
|1,713|H5 to H1|
|1,714|H5 to H2|
|1,715|H5 to H3|
|1,716|H5 to H4|
|1,717|H5 to H6|
|1,718|H5 to H7|
|1,719|H5 to H8|
|1,720|H6 to A6|
|1,721|H6 to B6|
|1,722|H6 to C1|
|1,723|H6 to C6|
|1,724|H6 to D2|
|1,725|H6 to D6|
|1,726|H6 to E3|
|1,727|H6 to E6|
|1,728|H6 to F4|
|1,729|H6 to F5|
|1,730|H6 to F6|
|1,731|H6 to F7|
|1,732|H6 to F8|
|1,733|H6 to G4|
|1,734|H6 to G5|
|1,735|H6 to G6|
|1,736|H6 to G7|
|1,737|H6 to G8|
|1,738|H6 to H1|
|1,739|H6 to H2|
|1,740|H6 to H3|
|1,741|H6 to H4|
|1,742|H6 to H5|
|1,743|H6 to H7|
|1,744|H6 to H8|
|1,745|H7 to A7|
|1,746|H7 to B1|
|1,747|H7 to B7|
|1,748|H7 to C2|
|1,749|H7 to C7|
|1,750|H7 to D3|
|1,751|H7 to D7|
|1,752|H7 to E4|
|1,753|H7 to E7|
|1,754|H7 to F5|
|1,755|H7 to F6|
|1,756|H7 to F7|
|1,757|H7 to F8|
|1,758|H7 to G5|
|1,759|H7 to G6|
|1,760|H7 to G7|
|1,761|H7 to G8|
|1,762|H7 to H1|
|1,763|H7 to H2|
|1,764|H7 to H3|
|1,765|H7 to H4|
|1,766|H7 to H5|
|1,767|H7 to H6|
|1,768|H7 to H8|
|1,769|H8 to A1|
|1,770|H8 to A8|
|1,771|H8 to B2|
|1,772|H8 to B8|
|1,773|H8 to C3|
|1,774|H8 to C8|
|1,775|H8 to D4|
|1,776|H8 to D8|
|1,777|H8 to E5|
|1,778|H8 to E8|
|1,779|H8 to F6|
|1,780|H8 to F7|
|1,781|H8 to F8|
|1,782|H8 to G6|
|1,783|H8 to G7|
|1,784|H8 to G8|
|1,785|H8 to H1|
|1,786|H8 to H2|
|1,787|H8 to H3|
|1,788|H8 to H4|
|1,789|H8 to H5|
|1,790|H8 to H6|
|1,791|H8 to H7|
|1,792|White King-side Castling|
|1,793|White Queen-side Castling|
|1,794|Black King-side Castling|
|1,795|Black Queen-side Castling|
|1,796|white A file pawn promoted to queen|
|1,797|white A file pawn promoted to rook|
|1,798|white A file pawn promoted to bishop|
|1,799|white A file pawn promoted to knight|
|1,800|white B file pawn promoted to queen|
|1,801|white B file pawn promoted to rook|
|1,802|white B file pawn promoted to bishop|
|1,803|white B file pawn promoted to knight|
|1,804|white C file pawn promoted to queen|
|1,805|white C file pawn promoted to rook|
|1,806|white C file pawn promoted to bishop|
|1,807|white C file pawn promoted to knight|
|1,808|white D file pawn promoted to queen|
|1,809|white D file pawn promoted to rook|
|1,810|white D file pawn promoted to bishop|
|1,811|white D file pawn promoted to knight|
|1,812|white E file pawn promoted to queen|
|1,813|white E file pawn promoted to rook|
|1,814|white E file pawn promoted to bishop|
|1,815|white E file pawn promoted to knight|
|1,816|white F file pawn promoted to queen|
|1,817|white F file pawn promoted to rook|
|1,818|white F file pawn promoted to bishop|
|1,819|white F file pawn promoted to knight|
|1,820|white G file pawn promoted to queen|
|1,821|white G file pawn promoted to rook|
|1,822|white G file pawn promoted to bishop|
|1,823|white G file pawn promoted to knight|
|1,824|white H file pawn promoted to queen|
|1,825|white H file pawn promoted to rook|
|1,826|white H file pawn promoted to bishop|
|1,827|white H file pawn promoted to knight|
|1,828|black A file pawn promoted to queen|
|1,829|black A file pawn promoted to rook|
|1,830|black A file pawn promoted to bishop|
|1,831|black A file pawn promoted to knight|
|1,832|black B file pawn promoted to queen|
|1,833|black B file pawn promoted to rook|
|1,834|black B file pawn promoted to bishop|
|1,835|black B file pawn promoted to knight|
|1,836|black C file pawn promoted to queen|
|1,837|black C file pawn promoted to rook|
|1,838|black C file pawn promoted to bishop|
|1,839|black C file pawn promoted to knight|
|1,840|black D file pawn promoted to queen|
|1,841|black D file pawn promoted to rook|
|1,842|black D file pawn promoted to bishop|
|1,843|black D file pawn promoted to knight|
|1,844|black E file pawn promoted to queen|
|1,845|black E file pawn promoted to rook|
|1,846|black E file pawn promoted to bishop|
|1,847|black E file pawn promoted to knight|
|1,848|black F file pawn promoted to queen|
|1,849|black F file pawn promoted to rook|
|1,850|black F file pawn promoted to bishop|
|1,851|black F file pawn promoted to knight|
|1,852|black G file pawn promoted to queen|
|1,853|black G file pawn promoted to rook|
|1,854|black G file pawn promoted to bishop|
|1,855|black G file pawn promoted to knight|
|1,856|black H file pawn promoted to queen|
|1,857|black H file pawn promoted to rook|
|1,858|black H file pawn promoted to bishop|
|1,859|black H file pawn promoted to knight|