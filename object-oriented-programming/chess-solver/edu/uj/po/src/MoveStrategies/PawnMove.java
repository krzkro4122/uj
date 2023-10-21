package edu.uj.po.src.MoveStrategies;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

import edu.uj.po.interfaces.ChessPiece;
import edu.uj.po.interfaces.Color;
import edu.uj.po.interfaces.Move;
import edu.uj.po.interfaces.Position;
import edu.uj.po.interfaces.Rank;
import edu.uj.po.src.Validator;
import edu.uj.po.src.Direction;
import edu.uj.po.src.Piece;
import edu.uj.po.src.interfaces.MoveStrategy;

public class PawnMove implements MoveStrategy {

    private Piece piece;

    public PawnMove(Piece piece) {
        this.piece = piece;
    }

    private boolean hasAlreadyMoved() {
        if (piece.getType() != ChessPiece.PAWN)
            throw new IllegalCallerException("This ain't no pawn! Stop calling me...");
        switch (piece.getColor()) {
            case WHITE: if (piece.getPosition().rank() == Rank.SECOND) return false;
            case BLACK: if (piece.getPosition().rank() == Rank.SEVENTH) return false;
        }
        return true;
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

    private List<Move> leftAttack() {
        List<Move> moves = new ArrayList<Move>();
        Direction direction = piece.getColor() == Color.WHITE ? Direction.NORTH_WEST : Direction.SOUTH_WEST;
        Optional<Position> leftAttackPossiblePosition = createPosition(1, direction);

        if (leftAttackPossiblePosition.isEmpty()) { return moves; }

        Position leftAttackPosition = leftAttackPossiblePosition.get();
        Optional<Piece> potentialPiece = piece.checkWhoIsAt(leftAttackPosition);
        if (potentialPiece.isPresent()) {
            Piece detectedPiece = potentialPiece.get();
            if (detectedPiece.getColor() != piece.getColor()) {
                moves.add(createMove(leftAttackPosition));
            }
        }

        return moves;
    }

    private List<Move> rightAttack() {
        List<Move> moves = new ArrayList<Move>();
        Direction direction = piece.getColor() == Color.WHITE ? Direction.NORTH_EAST : Direction.SOUTH_EAST;
        Optional<Position> rightAttackPossiblePosition = createPosition(1, direction);

        if (rightAttackPossiblePosition.isEmpty()) { return moves; }

        Position rightAttackPosition = rightAttackPossiblePosition.get();
        Optional<Piece> potentialPiece = piece.checkWhoIsAt(rightAttackPosition);
        if (potentialPiece.isPresent()) {
            Piece detectedPiece = potentialPiece.get();
            if (detectedPiece.getColor() != piece.getColor()) {
                moves.add(createMove(rightAttackPosition));
            }
        }
        return moves;
    }

    private List<Move> leftEnPassant() {
        List<Move> moves = new ArrayList<Move>();
        Direction direction = piece.getColor() == Color.WHITE ? Direction.NORTH_WEST : Direction.SOUTH_WEST;
        Direction enPassantDirection = piece.getColor() == Color.WHITE ? Direction.WEST : Direction.WEST;
        Optional<Position> leftAttackPossiblePosition = createPosition(1, direction);

        if (leftAttackPossiblePosition.isEmpty()) { return moves; }

        Position leftAttackPosition = leftAttackPossiblePosition.get();
        Optional<Position> leftEnPassantPossiblePosition = createPosition(1, enPassantDirection);
        Optional<Piece> potentiallyEnPassable = piece.checkWhoIsAt(leftEnPassantPossiblePosition.get());
        if (potentiallyEnPassable.isPresent()) {
            Piece detectedPiece = potentiallyEnPassable.get();
            boolean isAnEnemy = detectedPiece.getColor() != piece.getColor();
            boolean isEnPassable = detectedPiece.enPassable;
            if (isAnEnemy && isEnPassable) {
                moves.add(createMove(leftAttackPosition));
            }
        }

        return moves;
    }

    private List<Move> rightEnPassant() {
        List<Move> moves = new ArrayList<Move>();
        Direction direction = piece.getColor() == Color.WHITE ? Direction.NORTH_EAST : Direction.SOUTH_EAST;
        Direction enPassantDirection = piece.getColor() == Color.WHITE ? Direction.EAST : Direction.EAST;
        Optional<Position> rightAttackPossiblePosition = createPosition(1, direction);

        if (rightAttackPossiblePosition.isEmpty()) { return moves; }

        Position rightAttackPosition = rightAttackPossiblePosition.get();
        Optional<Position> rightEnPassantPossiblePosition = createPosition(1, enPassantDirection);
        Optional<Piece> potentiallyEnPassable = piece.checkWhoIsAt(rightEnPassantPossiblePosition.get());
        if (potentiallyEnPassable.isPresent()) {
            Piece detectedPiece = potentiallyEnPassable.get();
            if (detectedPiece.getColor() != piece.getColor() && detectedPiece.enPassable) {
                moves.add(createMove(rightAttackPosition));
            }
        }

        return moves;
    }

    private Direction getMoveDirection() {
        return piece.getColor() == Color.WHITE ? Direction.NORTH : Direction.SOUTH;
    }

    @Override
    public List<Move> discoverPossibleMoves(Piece piece) {
        this.piece = piece;
        List<Move> moves = new ArrayList<Move>();

        // Attacks
        moves.addAll(leftAttack());
        moves.addAll(rightAttack());

        // Attacks
        moves.addAll(leftEnPassant());
        moves.addAll(rightEnPassant());

        // Simple moves
        Optional<Position> possiblePositionInFront = createPosition(1, getMoveDirection());
        if (possiblePositionInFront.isEmpty()) { return moves; }

        Position positionInFront = possiblePositionInFront.get();
        Optional<Piece> potentialPiece = piece.checkWhoIsAt(positionInFront);
        if (potentialPiece.isEmpty()) {
            moves.add(createMove(positionInFront));
            possiblePositionInFront = createPosition(2, getMoveDirection());

            if (possiblePositionInFront.isEmpty()) { return moves; }

            positionInFront = possiblePositionInFront.get();
            potentialPiece = piece.checkWhoIsAt(positionInFront);
            if (!hasAlreadyMoved() && potentialPiece.isEmpty()) {
                moves.add(createMove(positionInFront));
            }
        }

        return moves;
    }
}
