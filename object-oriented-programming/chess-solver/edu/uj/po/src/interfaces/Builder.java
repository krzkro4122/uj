package edu.uj.po.src.interfaces;

import edu.uj.po.interfaces.ChessPiece;
import edu.uj.po.interfaces.Color;
import edu.uj.po.interfaces.Position;
import edu.uj.po.src.Piece;

public interface Builder {
    public void setColor(Color color);
    public void setType(ChessPiece type);
    public void setPosition(Position position);
    public Piece build();
}
