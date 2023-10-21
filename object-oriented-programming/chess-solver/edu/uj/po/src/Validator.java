package edu.uj.po.src;

import java.util.ArrayList;
import java.util.Optional;
import java.util.List;

import edu.uj.po.interfaces.Position;
import edu.uj.po.interfaces.File;
import edu.uj.po.interfaces.Move;
import edu.uj.po.interfaces.Rank;

public class Validator {

    public static Optional<Position> validatePositionBounds(Position currentPosition, Direction direction, int amount) {
        File startFile = currentPosition.file();
        Rank startRank = currentPosition.rank();
        File[] files = File.values();
        Rank[] ranks = Rank.values();

        int destinationFile = startFile.ordinal() + amount * direction.getFileCoef();
        int destinationRank = startRank.ordinal() + amount * direction.getRankCoef();
        if (    destinationFile >= files.length || destinationFile < 0
            ||  destinationRank >= ranks.length || destinationRank < 0)
            return Optional.empty();
        return Optional.of(new Position(
            files[destinationFile],
            ranks[destinationRank]
        ));
    }

    public static Optional<Position> validatePositionBounds(Position currentPosition, KnightDirection direction) {
        File startFile = currentPosition.file();
        Rank startRank = currentPosition.rank();
        File[] files = File.values();
        Rank[] ranks = Rank.values();

        int destinationFile = startFile.ordinal() + direction.getFileCoef();
        int destinationRank = startRank.ordinal() + direction.getRankCoef();
        if (    destinationFile >= files.length || destinationFile < 0
            ||  destinationRank >= ranks.length || destinationRank < 0)
            return Optional.empty();
        return Optional.of(new Position(
            files[destinationFile],
            ranks[destinationRank]
        ));
    }

    public static List<Move> pruneSuicidalMoves(Piece piece, List<Move> moves) {
        Position initialPosition = piece.getPosition();
        List<Move> prunedMoves =  new ArrayList<Move>(moves);

        for (Move move : moves) {
            Optional<Piece> possibleEnemyVictim = piece.setPosition(move.finalPosition());
            boolean kingInCheck = piece.isKingInCheck(piece.color);
            if (kingInCheck) { prunedMoves.remove(move); }
            piece.revertPosition(initialPosition, possibleEnemyVictim);
        }

        return prunedMoves;
    }
}
