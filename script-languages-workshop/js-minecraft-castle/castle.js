let makePlatform = (offset: number, radius: number, moatWidth: number, height: number) => {

    // Platform's foundation
    shapes.circle(Block.GrassPath, pos(offset, 0, 0), radius, Axis.Y, ShapeOperation.Replace);

    for (let i = 1; i < height; i++) {
        // Outer ring
        shapes.circle(Block.GrassPath, pos(offset, i, 0), radius, Axis.Y, ShapeOperation.Replace);
        // A moat between the rings
        shapes.circle(Block.Water, pos(offset, i, 0), radius - 1, Axis.Y, ShapeOperation.Replace);
        // Inner area
        shapes.circle(Block.GrassPath, pos(offset, i, 0), radius - 1 - moatWidth, Axis.Y, ShapeOperation.Replace);
    }

};

let makeFoundations = (offset: number, width: number, height: number, length: number, startHeight: number) => {

    for (let i = 0; i < height; i++) {

        for (let k = 0; k < length; k++) {
            shapes.line(STONE_BRICKS, pos(0, startHeight + i, k), pos(width, startHeight + i, k));
        }
    }
};



// MAIN
let main = () => {
    let myOffset = 25;
    let platformHeight = 3

    makePlatform(myOffset, 20, 4, platformHeight);
    makeFoundations(myOffset, 10, 2, 10, platformHeight);

};


// Chat bindings
player.onChat("run", () => {
    main();
})
player.onChat("clear", () => {
    console.log("TODO");
});
