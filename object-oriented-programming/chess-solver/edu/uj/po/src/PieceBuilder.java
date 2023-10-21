package edu.uj.po.src;

import edu.uj.po.interfaces.ChessPiece;
import edu.uj.po.interfaces.Color;
import edu.uj.po.interfaces.Position;
import edu.uj.po.src.MoveStrategies.*;
import edu.uj.po.src.interfaces.Builder;
import edu.uj.po.src.interfaces.MoveStrategy;

public class PieceBuilder implements Builder {

    private Color color;
    private ChessPiece type;
    private Position position;
    private Piece previous;
    private Piece root;

    public PieceBuilder() {}

    @Override
    public void setColor(Color color) {
        this.color = color;
    }

    @Override
    public void setType(ChessPiece type) {
        this.type = type;
    }

    @Override
    public void setPosition(Position position) {
        this.position = position;
    }

    private MoveStrategy determineMoveStrategy(Piece piece) {
        MoveStrategy typeStrategy;
        switch (type) {
            case KING:
                typeStrategy = new KingMove(piece);
                break;
            case ROOK:
                typeStrategy = new RookMove(piece);
                break;
            case BISHOP:
                typeStrategy = new BishopMove(piece);
                break;
            case QUEEN:
                typeStrategy = new QueenMove(piece);
                break;
            case KNIGHT:
                typeStrategy = new KnightMove(piece);
                break;
            case PAWN:
                typeStrategy = new PawnMove(piece);
                break;
            default:
                throw new IllegalArgumentException("Invalid piece type");
        }
        return typeStrategy;
    }

    @Override
    public Piece build() {
        Piece piece = new Piece();
        piece.type = type;
        piece.color = color;
        piece.position = position;
        piece.moveStrategy = determineMoveStrategy(piece);

        if (root == null) {
            root = piece;
        } else {
            previous.next = piece;
        }
        previous = piece;
        piece.root = root;

        return piece;
    }
}