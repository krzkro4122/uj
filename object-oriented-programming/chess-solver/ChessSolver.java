import java.util.List;
import java.util.Optional;

import edu.uj.po.interfaces.ChessPiece;
import edu.uj.po.interfaces.Color;
import edu.uj.po.interfaces.Move;
import edu.uj.po.interfaces.Position;
import edu.uj.po.interfaces.Setup;
import edu.uj.po.interfaces.Solver;
import edu.uj.po.src.Board;
import edu.uj.po.src.Piece;
import edu.uj.po.src.PieceBuilder;


public class ChessSolver implements Setup, Solver {

    private Board board;
    private PieceBuilder pieceBuilder;

    public ChessSolver() {
        reset();
    }

	public Optional<Move> findMateInOneMove(Color color) {
        return board.findMateInOneMove(color);
    }

	public Optional<Move> findStalemateInOneMove(Color color) {
        return board.findStalemateInOneMove(color);
    }

    @Override
    public void reset() {
        board = new Board();
        pieceBuilder = new PieceBuilder();
    }

    @Override
    public void addChessPiece(Position position, Color color, ChessPiece piece) {
        pieceBuilder.setColor(color);
        pieceBuilder.setPosition(position);
        pieceBuilder.setType(piece);
        Piece newPiece = pieceBuilder.build();
        board.addChessPiece(newPiece);
    }

    public List<Move> testMoveGeneration() {
        Piece firstPiece = board.getPieces().get(0);
        firstPiece.discoverPossibleMoves();
        return firstPiece.getPossibleMoves();
    }
}
