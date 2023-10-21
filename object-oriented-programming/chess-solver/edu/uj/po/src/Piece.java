package edu.uj.po.src;

import java.util.ArrayList;
import java.util.Optional;
import java.util.List;

import edu.uj.po.interfaces.ChessPiece;
import edu.uj.po.interfaces.Position;
import edu.uj.po.interfaces.Color;
import edu.uj.po.interfaces.Move;
import edu.uj.po.interfaces.Rank;
import edu.uj.po.src.MoveStrategies.QueenMove;
import edu.uj.po.src.interfaces.SearchHandler;
import edu.uj.po.src.interfaces.MoveStrategy;

public class Piece implements SearchHandler {

    Color color;
    ChessPiece type;
    Piece root, next;
    Position position;
    MoveStrategy moveStrategy;
    boolean isCaptured = false;
    boolean isPromoted = false;
    public boolean enPassable = false;
    private List<Move> possibleMoves;

    @Override
    public Optional<Move> findMate(Color color) {

        // Resp. chain filter
        if (getColor() != color) {
            return next == null ? Optional.empty() : next.findMate(color);
        }

        Color enemyColor = getOppositeColor();

        Position initialPosition = getPosition();
        for (Move move : getPossibleMoves()) {
            Optional<Piece> possibleEnemyVictim = setPosition(move.finalPosition());

            if (!isKingInCheck(enemyColor)) {
                revertPosition(initialPosition, possibleEnemyVictim);
                continue;
            }

            List<Move> enemyMovesThatPreventMate = new ArrayList<Move>();

            List<Piece> enemies = getEnemies();
            for (Piece enemy : enemies) {
                List<Move> enemyMoves = enemy.getPossibleMoves();
                Position enemyInitialPosition = enemy.getPosition();
                for (Move enemyMove : enemyMoves) {
                    Optional<Piece> possibleAlliedVictim = enemy.setPosition(enemyMove.finalPosition());
                    Piece enemyKing = getKing(enemyColor);
                    boolean enemyKingCantMove = enemyKing.getMoveStrategy().discoverPossibleMoves(enemyKing).isEmpty();
                    boolean enemyKingInCheckAndCantMove = isKingInCheck(enemyColor) && enemyKingCantMove;
                    if (!enemyKingInCheckAndCantMove) {
                        enemyMovesThatPreventMate.add(enemyMove);
                    }
                    enemy.revertPosition(enemyInitialPosition, possibleAlliedVictim);
                }
            }
            if (enemyMovesThatPreventMate.isEmpty()) {
                revertPosition(initialPosition, possibleEnemyVictim);
                return Optional.of(move);
            }
            revertPosition(initialPosition, possibleEnemyVictim);
        }

        // Resp. chain
        return next == null ? Optional.empty() : next.findMate(color);
    }

    @Override
    public Optional<Move> findStalemate(Color color) {

        // Resp. chain filter
        if (this.color != color) {
            return next == null ? Optional.empty() : next.findStalemate(color);
        }

        Position initialPosition = getPosition();
        for (Move move : getPossibleMoves()) {
            Optional<Piece> possibleEnemyVictim = setPosition(move.finalPosition());

            if (isKingInCheck(getOppositeColor())) {
                revertPosition(initialPosition, possibleEnemyVictim);
                continue;
            }

            List<Piece> enemies = getEnemies();
            List<Move> discoveredEnemyMoves = new ArrayList<Move>();
            for (Piece enemy : enemies) {
                possibleEnemyVictim = setPosition(move.finalPosition());
                discoveredEnemyMoves.addAll(enemy.getPossibleMoves());
                revertPosition(initialPosition, possibleEnemyVictim);
            }

            possibleEnemyVictim = setPosition(move.finalPosition());
            if (discoveredEnemyMoves.isEmpty() && !isKingInCheck(getOppositeColor())) {
                revertPosition(initialPosition, possibleEnemyVictim);
                return Optional.of(move);
            }
            revertPosition(initialPosition, possibleEnemyVictim);
        }

        // Resp. chain
        return next == null ? Optional.empty() : next.findStalemate(color);
    }

    public Position getPosition() {
        return position;
    }

    public Optional<Piece> setPosition(Position position) {
        Rank rankInitial = this.position.rank();
        int rankInitialInt = this.position.rank().ordinal();
        this.position = position;
        int rankFinal = this.position.rank().ordinal();

        // En passant
        if (type == ChessPiece.PAWN) {
            int rankDifference = rankFinal - rankInitialInt;
            if (rankDifference == 2) {
                enPassable = true;
                return Optional.empty();
            }
        }

        // Capture
        boolean capturedAnEnemy = false;
        Optional<Piece> possiblePiece = checkWhoIsAt(position);

        Position enPassantTribute = new Position(position.file(), rankInitial);
        Optional<Piece> possibleEnPassantTribute = checkWhoIsAt(enPassantTribute);
        boolean enPassantable = type == ChessPiece.PAWN && possiblePiece.isEmpty() && possibleEnPassantTribute.isPresent();

        if (possiblePiece.isPresent()) {
            Piece attackedPiece = possiblePiece.get();
            if (attackedPiece.getColor() != color) {
                attackedPiece.isCaptured = true;
                capturedAnEnemy = true;
            }
        }
        else if (enPassantable) {
            Piece attackedPiece = possibleEnPassantTribute.get();
            if (attackedPiece.getColor() != color) {
                attackedPiece.isCaptured = true;
                capturedAnEnemy = true;
            }
        }

        // Promotion
        if (type == ChessPiece.PAWN) {
            boolean whitePromotable = color == Color.WHITE && position.rank() == Rank.EIGHTH;
            boolean blackPromotable = color == Color.BLACK && position.rank() == Rank.FIRST;
            boolean promotable = whitePromotable || blackPromotable;
            if (promotable) {
                type = ChessPiece.QUEEN;
                isPromoted = true;
                moveStrategy = new QueenMove(this);
            }
        }

        if (possiblePiece.isPresent() && capturedAnEnemy) {
            return Optional.of(possiblePiece.get());
        } else if (possibleEnPassantTribute.isPresent() && capturedAnEnemy) {
            return Optional.of(possibleEnPassantTribute.get());
        } else return Optional.empty();
    }

    public void revertPosition (Position position, Optional<Piece> possibleVictim) {
        this.position = position;
        if (possibleVictim.isPresent()) {
            Piece victim = possibleVictim.get();
            victim.isCaptured = false;
        }
        if (isPromoted) {
            type = ChessPiece.PAWN;
            isPromoted = false;
        }
        if (type == ChessPiece.PAWN) {
            enPassable = false;
        }
    }

    public Color getColor() {
        return color;
    }

    public Color getOppositeColor() {
        return color == Color.WHITE ? Color.BLACK : Color.WHITE;
    }

    public Color getOppositeColor(Color color) {
        return color == Color.WHITE ? Color.BLACK : Color.WHITE;
    }

    public ChessPiece getType() {
        return type;
    }

    public Piece getRoot() {
        return root;
    }

    public MoveStrategy getMoveStrategy() {
        return moveStrategy;
    }

    public List<Move> getPossibleMoves() {
        discoverPossibleMoves();
        return possibleMoves;
    }

    public List<Piece> getColoredPieces(Color color) {
        List<Piece> coloredPieces = new ArrayList<Piece>();
        Piece currentPiece = root;
        while (currentPiece != null) {
            if (currentPiece.getColor() == color && !currentPiece.isCaptured) {
                coloredPieces.add(currentPiece);
            }
            currentPiece = currentPiece.next;
        }
        return coloredPieces;
    }

    public List<Piece> getAllies() {
        return getColoredPieces(color)
            .stream()
            .filter(piece -> !piece.isCaptured)
            .toList();
    }

    public List<Piece> getEnemies() {
        return getColoredPieces(getOppositeColor())
            .stream()
            .filter(piece -> !piece.isCaptured)
            .toList();
    }

    public Piece getKing(Color color) {
        Piece currentPiece = root;
        while (currentPiece != null) {
            boolean isKing = currentPiece.getType() == ChessPiece.KING;
            boolean isTheRightColor = currentPiece.getColor() == color;
            if (isKing && isTheRightColor) {
                return currentPiece;
            }
            currentPiece = currentPiece.next;
        }
        return null;
    }

    public Optional<Piece> checkWhoIsAt(Position position) {
        Piece currentPiece = root;
        while (currentPiece != null) {
            boolean isAtPosition = currentPiece.getPosition().equals(position);
            boolean isntCaptured = !currentPiece.isCaptured;
            boolean isntSelf = currentPiece != this;
            if ( isAtPosition && isntCaptured && isntSelf ) {
                return Optional.of(currentPiece);
            }
            currentPiece = currentPiece.next;
        }
        return Optional.empty();
    }

    public boolean isKingInCheck(Color color) {
        Position kingPosition = getKing(color).getPosition();
        List<Piece> enemies = getColoredPieces(getOppositeColor(color));
        for (Piece enemy : enemies) {
            List<Move> enemyMoves = enemy.getMoveStrategy().discoverPossibleMoves(enemy);
            for (Move enemyMove : enemyMoves) {
                if (enemyMove.finalPosition().equals(kingPosition)) {
                    return true;
                }
            }
        }
        return false;
    }

    public void discoverPossibleMoves() {
        possibleMoves = Validator.pruneSuicidalMoves(
            this,
            moveStrategy.discoverPossibleMoves(this)
        );
    }
}
