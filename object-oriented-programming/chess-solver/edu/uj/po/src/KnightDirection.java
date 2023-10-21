package edu.uj.po.src;

public enum KnightDirection {
    NORTH_EAST_DEPTH(1, 2),
    NORTH_EAST_BREADTH(2, 1),
    SOUTH_EAST_BREADTH(2, -1),
    SOUTH_EAST_DEPTH(1, -2),
    SOUTH_WEST_DEPTH(-1, -2),
    SOUTH_WEST_BREADTH(-2, -1),
    NORTH_WEST_BREADTH(-2, 1),
    NORTH_WEST_DEPTH(-1, 2);

    private int x;
    private int y;

    public int getFileCoef() {
        return x;
    }

    public int getRankCoef() {
        return y;
    }

    KnightDirection(int x, int y) {
        this.x = x;
        this.y = y;
    }

}
