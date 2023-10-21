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

public class RookMove implements MoveStrategy {

    private Piece piece;

    public RookMove(Piece piece) {
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
        for (int i = 1; i < 8; i++) {
            Optional<Position> possibleDestination = createPosition(i, direction);
            if (possibleDestination.isPresent()) {
                Position destination = possibleDestination.get();
                Optional<Piece> possiblePiece = piece.checkWhoIsAt(possibleDestination.get());
                if (possiblePiece.isEmpty()) {
                    moves.add(createMove(destination));
                } else if (possiblePiece.get().getColor() != piece.getColor()) {
                    moves.add(createMove(destination));
                    break;
                } else {
                    break;
                }
            } else break;
        }
        return moves;
    }

    @Override
    public List<Move> discoverPossibleMoves(Piece piece) {
        List<Move> moves = new ArrayList<Move>();
        List<Direction> rookDirections = List.of(Direction.NORTH, Direction.EAST, Direction.SOUTH, Direction.WEST);

        for (Direction direction : rookDirections) {
            moves.addAll(scanDirectionForMoves(direction));
        }

        return moves;
    }
}
