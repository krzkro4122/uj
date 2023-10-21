package edu.uj.po.src.interfaces;

import java.util.Optional;

import edu.uj.po.interfaces.Color;
import edu.uj.po.interfaces.Move;

public interface SearchHandler {
    public Optional<Move> findMate(Color color);
    public Optional<Move> findStalemate(Color color);
}
