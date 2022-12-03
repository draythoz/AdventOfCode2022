var lines = File.ReadLines(@"C:/tmp/aoc/2/input.txt");
var strategy = new List<RoundStrategy>();

foreach (var line in lines)
    strategy.Add(new RoundStrategy
    {
        ElfWillPlay = new RpsShape(line[0]),
        RoundResult = line[2]
    });

var rpsTournamentScore = strategy.Select(s => s.TotalScore).Sum();

Console.WriteLine($"Rps Tournament Score = {rpsTournamentScore}");

internal class RoundStrategy
{
    public char RoundResult { get; set; }
    public RpsShape ElfWillPlay { get; set; }

    public RpsShape IShouldPlay
    {
        get
        {
            switch (RoundResult)
            {
                case 'X': //need to lose
                    if (ElfWillPlay.ShapeGlyph == 'A') return new RpsShape('C');
                    if (ElfWillPlay.ShapeGlyph == 'B') return new RpsShape('A');
                    return new RpsShape('B');
                case 'Y': //need to draw
                    return new RpsShape(ElfWillPlay.ShapeGlyph);
                case 'Z': //need to win
                    if (ElfWillPlay.ShapeGlyph == 'A') return new RpsShape('B');
                    if (ElfWillPlay.ShapeGlyph == 'B') return new RpsShape('C');
                    return new RpsShape('A');
            }

            return new RpsShape('F');
        }
    }


    public int TotalScore
    {
        get
        {
            var score = 0;
            switch (RoundResult)
            {
                case 'X':
                    return IShouldPlay.ShapeScore;
                case 'Y':
                    return IShouldPlay.ShapeScore + 3;
                case 'Z':
                    return IShouldPlay.ShapeScore + 6;
                default:
                    return 0;
            }
        }
    }

    private int GetResultScore(RpsShape elf, RpsShape me)
    {
        //A = rock
        //B = paper
        //C = scissor
        //X = Rock - 1 point
        //Y = Paper - 2 points
        //Z = Scissors - 3 points

        switch (elf.ShapeGlyph)
        {
            case 'A':
                if (me.ShapeGlyph == 'X') return 3;
                if (me.ShapeGlyph == 'Y') return 6;
                return 0;
            case 'B':
                if (me.ShapeGlyph == 'X') return 0;
                if (me.ShapeGlyph == 'Y') return 3;
                return 6;
            case 'C':
                if (me.ShapeGlyph == 'X') return 6;
                if (me.ShapeGlyph == 'Y') return 0;
                return 3;
        }

        return 0;
    }
}

internal class RpsShape
{
    public RpsShape(char glyph)
    {
        ShapeGlyph = glyph;
    }

    public char ShapeGlyph { get; set; }

    public int ShapeScore => GetShapeScore(ShapeGlyph);

    private int GetShapeScore(char input)
    {
        switch (input)
        {
            case 'A':
                return 1;
            case 'B':
                return 2;
            case 'C':
                return 3;
        }

        return 0;
    }
}