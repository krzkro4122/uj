package edu.uj.po.src;

import java.util.List;
import java.util.Optional;
import java.util.ArrayList;

import edu.uj.po.interfaces.Move;
import edu.uj.po.interfaces.Color;
import edu.uj.po.interfaces.Solver;

public class Board implements Solver {
    private List<Piece> pieces;
    private BoardMemento memento;

    public Board() {
        pieces = new ArrayList<Piece>();
        memento = new BoardMemento(pieces);
    }

    public void loadSnapshot(BoardMemento memento) {
        pieces = memento.getBoard();
    }

    public BoardMemento saveSnapshot() {
        memento = new BoardMemento(pieces);
        return memento;
    }

    public List<Piece> getPieces() {
        return pieces;
    }

    public List<Piece> getColoredPieces(Color color) {
        return pieces
            .stream()
            .filter(p -> p.getColor() == color)
            .toList();
    }

    public void addChessPiece(Piece piece) {
        pieces.add(piece);
    }

    @Override
    public Optional<Move> findMateInOneMove(Color color) {
        Piece coloredRepresentant = pieces
            .stream()
            .filter(p -> p.getColor() == color)
            .findFirst()
            .get();
        return coloredRepresentant.root.findMate(color);
    }

    @Override
    public Optional<Move> findStalemateInOneMove(Color color) {
        Piece coloredRepresentant = pieces
            .stream()
            .filter(p -> p.getColor() == color)
            .findFirst()
            .get();
        return coloredRepresentant.root.findStalemate(color);
    }
}
