using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TimHanewich.Chess;
using TimHanewich.Chess.PGN;
using System.Collections.Generic;

namespace ChessAI
{
    public class InputOutputPrepTools
    {
        private StandardMove[] standardmoves;

        public InputOutputPrepTools(string standard_moves_json)
        {
            StandardMove[]? stdmvs = JsonConvert.DeserializeObject<StandardMove[]>(standard_moves_json);
            if (stdmvs != null)
            {
                standardmoves = stdmvs;
            }
            else
            {
                throw new Exception("Supplied JSON is not an array of standard moves");
            }
        }

        public float[] PrepareInputs(BoardPosition bp)
        {

            //Set up with the first 832 0's that will represent the board state (pieces on the board)
            List<float> ToReturn = new List<float>();
            for (int t = 0; t < 832; t++)
            {
                ToReturn.Add(0.0f);
            }
            
            //Seed with values
            int on_position_index = 0;
            foreach (Position p in Enum.GetValues(typeof(Position)))
            {
                Piece occ = bp.FindOccupyingPiece(p);
                if (occ == null)
                {
                    ToReturn[on_position_index * 13] = 1.0f;
                }
                else
                {
                    if (occ.Color == Color.White)
                    {
                        if (occ.Type == PieceType.Pawn)
                        {
                            ToReturn[(on_position_index * 13) + 1] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Knight)
                        {
                            ToReturn[(on_position_index * 13) + 2] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Bishop)
                        {
                            ToReturn[(on_position_index * 13) + 3] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Rook)
                        {
                            ToReturn[(on_position_index * 13) + 4] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Queen)
                        {
                            ToReturn[(on_position_index * 13) + 5] = 1.0f;
                        }
                        else if (occ.Type == PieceType.King)
                        {
                            ToReturn[(on_position_index * 13) + 6] = 1.0f;
                        }
                    }
                    else if (occ.Color == Color.Black)
                    {
                        if (occ.Type == PieceType.Pawn)
                        {
                            ToReturn[(on_position_index * 13) + 7] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Knight)
                        {
                            ToReturn[(on_position_index * 13) + 8] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Bishop)
                        {
                            ToReturn[(on_position_index * 13) + 9] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Rook)
                        {
                            ToReturn[(on_position_index * 13) + 10] = 1.0f;
                        }
                        else if (occ.Type == PieceType.Queen)
                        {
                            ToReturn[(on_position_index * 13) + 11] = 1.0f;
                        }
                        else if (occ.Type == PieceType.King)
                        {
                            ToReturn[(on_position_index * 13) + 12] = 1.0f;
                        }
                    }
                }
                on_position_index = on_position_index + 1;
            }
        
            //Who is to move?
            if (bp.ToMove == Color.White)
            {
                ToReturn.Add(1.0f);
                ToReturn.Add(0.0f);
            }
            else if (bp.ToMove == Color.Black)
            {
                ToReturn.Add(0.0f);
                ToReturn.Add(1.0f);
            }

            //Castling availability
            if (bp.WhiteKingSideCastlingAvailable)
            {
                ToReturn.Add(1.0f);
            }
            else
            {
                ToReturn.Add(0.0f);
            }
            if (bp.WhiteQueenSideCastlingAvailable)
            {
                ToReturn.Add(1.0f);
            }
            else
            {
                ToReturn.Add(0.0f);
            }
            if (bp.BlackKingSideCastlingAvailable)
            {
                ToReturn.Add(1.0f);
            }
            else
            {
                ToReturn.Add(0.0f);
            }
            if (bp.BlackQueenSideCastlingAvailable)
            {
                ToReturn.Add(1.0f);
            }
            else
            {
                ToReturn.Add(0.0f);
            }

            //En passant
            float[] en_passant_portion = new float[]{0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f};
            if (bp.EnPassantTarget.HasValue)
            {
                switch (bp.EnPassantTarget.Value)
                {
                    case Position.A3:
                        en_passant_portion[0] = 1.0f;
                        break;
                    case Position.B3:
                        en_passant_portion[1] = 1.0f;
                        break;
                    case Position.C3:
                        en_passant_portion[2] = 1.0f;
                        break;
                    case Position.D3:
                        en_passant_portion[3] = 1.0f;
                        break;
                    case Position.E3:
                        en_passant_portion[4] = 1.0f;
                        break;
                    case Position.F3:
                        en_passant_portion[5] = 1.0f;
                        break;
                    case Position.G3:
                        en_passant_portion[6] = 1.0f;
                        break;
                    case Position.H3:
                        en_passant_portion[7] = 1.0f;
                        break;
                    case Position.A6:
                        en_passant_portion[8] = 1.0f;
                        break;
                    case Position.B6:
                        en_passant_portion[9] = 1.0f;
                        break;
                    case Position.C6:
                        en_passant_portion[10] = 1.0f;
                        break;
                    case Position.D6:
                        en_passant_portion[11] = 1.0f;
                        break;
                    case Position.E6:
                        en_passant_portion[12] = 1.0f;
                        break;
                    case Position.F6:
                        en_passant_portion[13] = 1.0f;
                        break;
                    case Position.G6:
                        en_passant_portion[14] = 1.0f;
                        break;
                    case Position.H6:
                        en_passant_portion[15] = 1.0f;
                        break;
                }
            }
            ToReturn.AddRange(en_passant_portion);


            return ToReturn.ToArray();
        }

        public int SelectAppropriateOutputNeuronIndex(BoardPosition bp, Move m, Color c)
        {
            //Is it castling?
            if (m.Castling.HasValue)
            {
                if (m.Castling.Value == CastlingType.KingSide && c == Color.White)
                {
                    return 1792;
                }
                else if (m.Castling.Value == CastlingType.QueenSide && c == Color.White)
                {
                    return 1793;
                }
                else if (m.Castling.Value == CastlingType.KingSide && c == Color.Black)
                {
                    return 1794;
                }
                else if (m.Castling.Value == CastlingType.QueenSide && c == Color.Black)
                {
                    return 1795;
                }
            }

            //Is it a pawn promotion?
            if (m.IsPawnPromotion(bp))
            {
                if (c == Color.White)
                {
                    if (m.ToPosition.File() == 'A')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1796;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1797;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1798;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1799;
                        }
                    }
                    else if (m.ToPosition.File() == 'B')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1800;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1801;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1802;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1803;
                        }
                    }
                    else if (m.ToPosition.File() == 'C')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1804;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1805;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1806;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1807;
                        }
                    }
                    else if (m.ToPosition.File() == 'D')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1808;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1809;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1810;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1811;
                        }
                    }
                    else if (m.ToPosition.File() == 'E')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1812;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1813;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1814;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1815;
                        }
                    }
                    else if (m.ToPosition.File() == 'F')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1816;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1817;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1818;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1819;
                        }
                    }
                    else if (m.ToPosition.File() == 'G')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1820;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1821;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1822;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1823;
                        }
                    }
                    else if (m.ToPosition.File() == 'H')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1824;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1825;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1826;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1827;
                        }
                    }
                }
                else if (c == Color.Black)
                {
                    if (m.ToPosition.File() == 'A')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1828;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1829;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1830;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1831;
                        }
                    }
                    else if (m.ToPosition.File() == 'B')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1832;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1833;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1834;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1835;
                        }
                    }
                    else if (m.ToPosition.File() == 'C')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1836;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1837;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1838;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1839;
                        }
                    }
                    else if (m.ToPosition.File() == 'D')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1840;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1841;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1842;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1843;
                        }
                    }
                    else if (m.ToPosition.File() == 'E')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1844;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1845;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1846;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1847;
                        }
                    }
                    else if (m.ToPosition.File() == 'F')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1848;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1849;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1850;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1851;
                        }
                    }
                    else if (m.ToPosition.File() == 'G')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1852;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1853;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1854;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1855;
                        }
                    }
                    else if (m.ToPosition.File() == 'H')
                    {
                        if (m.PromotePawnTo == PieceType.Queen)
                        {
                            return 1856;
                        }
                        else if (m.PromotePawnTo == PieceType.Rook)
                        {
                            return 1857;
                        }
                        else if (m.PromotePawnTo == PieceType.Bishop)
                        {
                            return 1858;
                        }
                        else if (m.PromotePawnTo == PieceType.Knight)
                        {
                            return 1859;
                        }
                    }
                }
            }

            //If it neither of the two above, it is a standard move from point A to point B... so find that
            for (int t = 0; t < standardmoves.Length; t++)
            {
                StandardMove ThisMove = standardmoves[t];
                if (ThisMove.from == m.FromPosition && ThisMove.to == m.ToPosition)
                {
                    return t;
                }
            }

            //If we got this far, the move did not fit into any of the possible moves we've considered
            throw new Exception("Unable to find matching output neuron for move '" + m.FromPosition.ToString() + " to " + m.ToPosition.ToString() + "!");
        }
    
        public float[] PrepareOutputs(int selected_output_neuron_index)
        {
            List<float> ToReturn = new List<float>();
            for (int t = 0; t < 1860; t++)
            {
                ToReturn.Add(0.0f);
            }

            //Set
            ToReturn[selected_output_neuron_index] = 1.0f;

            return ToReturn.ToArray();
        }
    
        //Since both the input and output arrays are just a series of 0.0's with a few 1.0's in there, we can "compress" by assuming every part of the array will be a 0.0 and only specifying which indexes are a 1.0
        //The program that READS this data will have to know the number of values should be in each array (input vs output sequence), but that shouldn't be a problem.
        public int[] Compress(float[] arr)
        {
            List<int> ToReturn = new List<int>();
            for (int t = 0; t < arr.Length; t++)
            {
                if (arr[t] == 1.0f)
                {
                    ToReturn.Add(t);
                }
            }
            return ToReturn.ToArray();
        }
    }
}