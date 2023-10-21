package edu.uj.po.src.MoveStrategies;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import edu.uj.po.interfaces.Move;
import edu.uj.po.interfaces.Position;
import edu.uj.po.src.Validator;
import edu.uj.po.src.KnightDirection;
import edu.uj.po.src.Piece;
import edu.uj.po.src.interfaces.MoveStrategy;

public class KnightMove implements MoveStrategy {

    private Piece piece;

    public KnightMove(Piece piece) {
        this.piece = piece;
    }

    private Optional<Position> createPosition(KnightDirection direction) {
        return Validator.validatePositionBounds(piece.getPosition(), direction);
    }

    private Move createMove(Position destination) {
        return new Move(
            piece.getPosition(),
            destination
        );
    }

    @Override
    public List<Move> discoverPossibleMoves(Piece piece) {
        List<Move> moves = new ArrayList<Move>();

        for (KnightDirection direction : KnightDirection.values()) {
            Optional<Position> possibleDestination = createPosition(direction);
            if (possibleDestination.isPresent()) {
                Position destination = possibleDestination.get();
                Optional<Piece> possiblePiece = piece.checkWhoIsAt(destination);
                if (possiblePiece.isEmpty()) {
                    moves.add(createMove(destination));
                } else if (possiblePiece.get().getColor() != this.piece.getColor()) {
                    moves.add(createMove(destination));
                }
            }
        }

        return moves;
    }
}
