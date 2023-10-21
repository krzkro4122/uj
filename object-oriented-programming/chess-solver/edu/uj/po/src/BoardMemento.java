package edu.uj.po.src;

import java.util.ArrayList;
import java.util.List;

public class BoardMemento {
    private List<Piece> pieces;

    public BoardMemento(List<Piece> pieces) {
        this.pieces = new ArrayList<Piece>();
        for (Piece piece : pieces) {
            this.pieces.add(deepCopyPiece(piece));
        }
    }

    public Piece deepCopyPiece(Piece piece) {
        PieceBuilder pieceBuilder = new PieceBuilder();
        pieceBuilder.setColor(piece.getColor());
        pieceBuilder.setPosition(piece.getPosition());
        pieceBuilder.setType(piece.getType());
        return pieceBuilder.build();
    }

    public List<Piece> getBoard() {
        return pieces;
    }
}
