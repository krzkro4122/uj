package edu.uj.po.src.MoveStrategies;

import java.util.Optional;
import java.util.ArrayList;
import java.util.List;

import edu.uj.po.interfaces.Position;
import edu.uj.po.interfaces.Move;
import edu.uj.po.src.Validator;
import edu.uj.po.src.Direction;
import edu.uj.po.src.Piece;
import edu.uj.po.src.interfaces.MoveStrategy;

public class KingMove implements MoveStrategy {

    private Piece piece;

    public KingMove(Piece piece) {
        this.piece = piece;
    }

    private Optional<Position> createPosition(int amount, Direction direction) {
        return Validator.validatePositionBounds(piece.getPosition(), direction, amount);
    }

    private Move createMove(Position destination) {
        return new Move(
            piece.getPosition(),
            destination
        );
    }

    private List<Move> scanDirectionForMoves(Direction direction) {
        List<Move> moves = new ArrayList<Move>();
        Optional<Position> possibleDestination = createPosition(1, direction);
        if (possibleDestination.isPresent()) {
            Position destination = possibleDestination.get();
            Optional<Piece> possiblePiece = piece.checkWhoIsAt(possibleDestination.get());
            if (possiblePiece.isEmpty()) {
                moves.add(createMove(destination));
            } else if (possiblePiece.get().getColor() != piece.getColor()) {
                moves.add(createMove(destination));
            }
        }
        return moves;
    }

    @Override
    public List<Move> discoverPossibleMoves(Piece piece) {
        List<Move> moves = new ArrayList<Move>();
        List<Direction> directions = List.of(
            Direction.NORTH_EAST,   Direction.SOUTH_EAST,   Direction.SOUTH_WEST,   Direction.NORTH_WEST,
            Direction.NORTH,        Direction.EAST,         Direction.SOUTH,        Direction.WEST
        );

        for (Direction direction : directions) {
            moves.addAll(scanDirectionForMoves(direction));
        }

        return moves;
    }
}
