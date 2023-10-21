package edu.uj.po.src.interfaces;

import java.util.List;

import edu.uj.po.interfaces.Move;
import edu.uj.po.src.Piece;

public interface MoveStrategy {
    List<Move> discoverPossibleMoves(Piece piece);
}
