using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LiteralMaze;
using UnityEngine;
using UnityEngine.UI;
using Rnd = UnityEngine.Random;

public class literalMazeScript : MonoBehaviour
{
    private static readonly string[] _wordlist =
    {
        "ABLE", "ACHE", "ACID", "ACNE", "ACRE", "AFAR", "AFRO", "AGED", "AIDE", "AKIN", "ALAS", "ALLY", "ALSO", "AMEN", "AMID", "APEX", "AQUA", "ARCH", "AREA", "ARID", "ARMY", "ATOM", "AUNT", "AURA", "AWAY", "AXES", "AXIS", "AXLE",
        "BABY", "BACK", "BAIL", "BAIT", "BAKE", "BALD", "BALL", "BAND", "BANG", "BANK", "BARE", "BARK", "BARN", "BASE", "BASS", "BATH", "BATS", "BEAD", "BEAM", "BEAN", "BEAR", "BEAT", "BEEF", "BEEN", "BEER", "BELL", "BELT", "BEND", "BENT", "BEST", "BIAS", "BIKE", "BILE", "BILL", "BIND", "BIRD", "BITE", "BLAH", "BLEW", "BLOW", "BLUE", "BLUR", "BOAR", "BOAT", "BODY", "BOIL", "BOLD", "BOLT", "BOMB", "BOND", "BONE", "BONY", "BOOK", "BOOM", "BOOT", "BORE", "BORN", "BOSS", "BOTH", "BOUT", "BOWL", "BROW", "BULB", "BULK", "BULL", "BUMP", "BURN", "BURY", "BUSH", "BUST", "BUSY", "BUTT", "BUZZ",
        "CAFE", "CAGE", "CAKE", "CALF", "CALL", "CALM", "CAME", "CAMP", "CANE", "CAPE", "CARD", "CARE", "CARP", "CART", "CASE", "CASH", "CAST", "CAVE", "CELL", "CHAP", "CHAT", "CHEF", "CHIN", "CHIP", "CHOP", "CITY", "CLAD", "CLAM", "CLAN", "CLAP", "CLAW", "CLAY", "CLIP", "CLOG", "CLUB", "CLUE", "COAL", "COAT", "COCK", "CODE", "COIL", "COIN", "COLD", "COMB", "COME", "CONE", "COOK", "COOL", "COPE", "COPY", "CORD", "CORE", "CORK", "CORN", "COST", "COSY", "COUP", "COZY", "CRAB", "CREW", "CRIB", "CROP", "CROW", "CUBE", "CULT", "CURB", "CURE", "CURL", "CUTE",
        "DAFT", "DAMP", "DARE", "DARK", "DART", "DASH", "DATA", "DATE", "DAWN", "DAYS", "DEAD", "DEAF", "DEAL", "DEAR", "DEBT", "DECK", "DEED", "DEEP", "DEER", "DENT", "DENY", "DESK", "DIAL", "DICE", "DIET", "DINE", "DIRE", "DIRT", "DISC", "DISH", "DISK", "DIVE", "DOCK", "DOLE", "DOLL", "DOME", "DONE", "DOOR", "DOSE", "DOVE", "DOWN", "DRAG", "DRAW", "DREW", "DRIP", "DROP", "DRUG", "DRUM", "DUAL", "DUCK", "DUEL", "DUET", "DULL", "DULY", "DUMB", "DUMP", "DUSK", "DUST", "DUTY",
        "EACH", "EARN", "EARS", "EASE", "EAST", "EASY", "EATS", "ECHO", "EDGE", "EDIT", "ELSE", "ENVY", "EPIC", "EURO", "EVEN", "EVER", "EVIL", "EXAM", "EXIT", "EYED", "EYES",
        "FACE", "FACT", "FADE", "FAIL", "FAIR", "FAKE", "FALL", "FAME", "FARE", "FARM", "FAST", "FATE", "FEAR", "FEAT", "FEED", "FEEL", "FEET", "FELL", "FELT", "FILE", "FILL", "FILM", "FIND", "FINE", "FIRE", "FIRM", "FISH", "FIST", "FIVE", "FLAG", "FLAP", "FLAT", "FLAW", "FLED", "FLEE", "FLEW", "FLEX", "FLIP", "FLOW", "FLUX", "FOAM", "FOIL", "FOLD", "FOLK", "FOND", "FONT", "FOOD", "FOOL", "FOOT", "FORD", "FORK", "FORM", "FORT", "FOUL", "FOUR", "FREE", "FROG", "FROM", "FUEL", "FULL", "FUND", "FURY", "FUSE", "FUSS",
        "GAIN", "GALA", "GALL", "GAME", "GANG", "GASP", "GATE", "GAVE", "GAZE", "GEAR", "GENE", "GERM", "GIFT", "GILL", "GILT", "GIRL", "GIVE", "GLAD", "GLEE", "GLOW", "GLUE", "GOAL", "GOAT", "GOES", "GOLD", "GOLF", "GONE", "GONG", "GOOD", "GOSH", "GOWN", "GRAB", "GRAM", "GRAY", "GREW", "GREY", "GRID", "GRIM", "GRIN", "GRIP", "GRIT", "GROW", "GUST",
        "HAIL", "HAIR", "HALF", "HALL", "HALT", "HAND", "HANG", "HARD", "HARE", "HARM", "HATE", "HAUL", "HAVE", "HAWK", "HAZE", "HEAD", "HEAL", "HEAP", "HEAR", "HEAT", "HEEL", "HEIR", "HELD", "HELL", "HELP", "HERB", "HERD", "HERE", "HERO", "HERS", "HIDE", "HIGH", "HIKE", "HILL", "HINT", "HIRE", "HOLD", "HOLE", "HOLY", "HOME", "HOOD", "HOOK", "HOPE", "HORN", "HOSE", "HOST", "HOUR", "HOWL", "HUGE", "HULL", "HUNG", "HUNT", "HURT", "HUSH", "HYMN", "HYPE",
        "ICED", "ICON", "IDEA", "IDLE", "IDOL", "INCH", "INFO", "INTO", "IRON", "ITCH", "ITEM",
        "JACK", "JAIL", "JARS", "JAZZ", "JINX", "JOBS", "JOIN", "JOKE", "JUMP", "JUNK", "JURY", "JUST",
        "KEEN", "KEEP", "KELP", "KEPT", "KICK", "KILL", "KIND", "KING", "KISS", "KITE", "KIWI", "KNEE", "KNEW", "KNIT", "KNOB", "KNOT", "KNOW",
        "LACE", "LACK", "LADY", "LAID", "LAIR", "LAKE", "LAMB", "LAMP", "LAND", "LANE", "LAST", "LATE", "LAVA", "LAWN", "LAZY", "LEAD", "LEAF", "LEAK", "LEAN", "LEAP", "LEFT", "LEND", "LENS", "LESS", "LEST", "LEVY", "LIAR", "LIED", "LIFE", "LIFT", "LIKE", "LIMB", "LIME", "LINE", "LINK", "LION", "LIST", "LIVE", "LOAD", "LOAF", "LOAN", "LOCK", "LOFT", "LOGO", "LONE", "LONG", "LOOK", "LOOP", "LORD", "LOSE", "LOSS", "LOST", "LOTS", "LOUD", "LOVE", "LUCK", "LUMP", "LUNG", "LURE", "LUSH", "LUST",
        "MADE", "MAID", "MAIL", "MAIN", "MAKE", "MALE", "MALL", "MALT", "MANY", "MARE", "MARK", "MASK", "MASS", "MAST", "MATE", "MATH", "MAZE", "MEAL", "MEAN", "MEAT", "MEET", "MELT", "MEME", "MEMO", "MENU", "MERE", "MESH", "MESS", "MICE", "MILD", "MILE", "MILK", "MILL", "MIME", "MIND", "MINE", "MINI", "MINT", "MISS", "MIST", "MOAT", "MOCK", "MODE", "MOLD", "MOLE", "MONK", "MOOD", "MOON", "MOOR", "MORE", "MOSS", "MOST", "MOTH", "MOVE", "MUCH", "MULE", "MUST", "MUTE", "MYTH",
        "NAIL", "NAME", "NAVE", "NEAR", "NEAT", "NECK", "NEED", "NEON", "NEST", "NEWS", "NEWT", "NEXT", "NICE", "NICK", "NINE", "NODE", "NONE", "NOOK", "NOON", "NORM", "NOSE", "NOTE", "NOUN", "NUMB", "NUTS",
        "OATH", "OATS", "OBEY", "OBOE", "ODDS", "ODOR", "OGRE", "OILY", "OINK", "OKAY", "OMEN", "OMIT", "ONCE", "ONLY", "ONTO", "OOZE", "OPEN", "ORAL", "ORCA", "ORES", "OURS", "OVAL", "OVEN", "OVER",
        "PACE", "PACK", "PACT", "PAGE", "PAID", "PAIN", "PAIR", "PALE", "PALM", "PARK", "PART", "PASS", "PAST", "PATH", "PAWN", "PEAK", "PEAR", "PEAT", "PEEK", "PEEL", "PEER", "PERK", "PEST", "PICK", "PIER", "PILE", "PILL", "PINE", "PINK", "PINT", "PIPE", "PITY", "PLAN", "PLAY", "PLEA", "PLOT", "PLOW", "PLOY", "PLUG", "PLUM", "PLUS", "POEM", "POET", "POKE", "POLE", "POLL", "POLO", "POND", "PONY", "POOL", "POOR", "PORK", "PORT", "POSE", "POSH", "POST", "POUR", "PRAY", "PREY", "PROP", "PULL", "PUMP", "PUNK", "PURE", "PUSH",
        "QUAY", "QUID", "QUIT", "QUIZ",
        "RACE", "RACK", "RAFT", "RAGE", "RAID", "RAIL", "RAIN", "RAKE", "RAMP", "RANK", "RARE", "RASH", "RATE", "READ", "REAL", "REAR", "REEF", "RELY", "RENT", "REST", "RICE", "RICH", "RIDE", "RIFT", "RING", "RIOT", "RIPE", "RISE", "RISK", "RITE", "ROAD", "ROAM", "ROAR", "ROBE", "ROCK", "RODE", "ROLE", "ROLL", "ROOF", "ROOM", "ROOT", "ROPE", "ROSE", "ROSY", "RUBY", "RUDE", "RUIN", "RULE", "RUNG", "RUSH", "RUST",
        "SACK", "SAFE", "SAGA", "SAID", "SAIL", "SAKE", "SALE", "SALT", "SAME", "SAND", "SANE", "SANG", "SANK", "SAVE", "SCAN", "SCAR", "SCUM", "SEAL", "SEAM", "SEAT", "SEED", "SEEK", "SEEM", "SEEN", "SELF", "SELL", "SEND", "SENT", "SEXY", "SHED", "SHIP", "SHOE", "SHOP", "SHOT", "SHOW", "SHUT", "SICK", "SIDE", "SIGH", "SIGN", "SILK", "SING", "SINK", "SITE", "SIZE", "SKIN", "SKIP", "SLAB", "SLAM", "SLID", "SLIM", "SLIP", "SLOT", "SLOW", "SLUM", "SMUG", "SNAP", "SNOW", "SOAP", "SOAR", "SODA", "SOFA", "SOFT", "SOIL", "SOLD", "SOLE", "SOLO", "SOME", "SONG", "SOON", "SORE", "SORT", "SOUL", "SOUP", "SOUR", "SPAN", "SPIN", "SPOT", "SPUN", "SPUR", "STAB", "STAR", "STAY", "STEM", "STEP", "STIR", "STOP", "SUCH", "SUIT", "SUNG", "SUNK", "SURE", "SWAN", "SWAP", "SWIM",
        "TACK", "TACO", "TAIL", "TAKE", "TALE", "TALK", "TALL", "TAME", "TANK", "TAPE", "TASK", "TAUT", "TAXI", "TEAL", "TEAM", "TEAR", "TELL", "TEND", "TENT", "TERM", "TEST", "TEXT", "THAN", "THAT", "THAW", "THEE", "THEM", "THEN", "THEY", "THIN", "THIS", "THOU", "THUD", "THUS", "TIDE", "TIDY", "TIED", "TIER", "TILE", "TILL", "TILT", "TIME", "TINY", "TIRE", "TOAD", "TOIL", "TOLD", "TOLL", "TOMB", "TONE", "TOOK", "TOOL", "TORE", "TORN", "TORT", "TORY", "TOSS", "TOUR", "TOWN", "TRAM", "TRAP", "TRAY", "TREE", "TRIM", "TRIO", "TRIP", "TRUE", "TSAR", "TUBE", "TUCK", "TUNA", "TUNE", "TURF", "TURN", "TWIN", "TYPE",
        "UGLY", "UNDO", "UNIT", "UNTO", "UPON", "URGE", "USED", "USER", "USES",
        "VAIN", "VARY", "VASE", "VAST", "VEIL", "VEIN", "VENT", "VERB", "VERY", "VEST", "VETO", "VIAL", "VIEW", "VILE", "VINE", "VISA", "VOID", "VOTE",
        "WADE", "WAGE", "WAIT", "WAKE", "WALK", "WALL", "WAND", "WANT", "WARD", "WARM", "WARN", "WARP", "WARY", "WASH", "WAVE", "WAVY", "WAXY", "WEAK", "WEAR", "WEED", "WEEK", "WELD", "WELL", "WENT", "WERE", "WEST", "WHAT", "WHEN", "WHIP", "WHOM", "WIDE", "WIFE", "WILD", "WILL", "WIND", "WINE", "WING", "WIPE", "WIRE", "WISE", "WISH", "WITH", "WOLF", "WOMB", "WOOD", "WOOL", "WORD", "WORE", "WORK", "WORM", "WORN", "WRAP", "WRIT",
        "XYLO",
        "YARD", "YARN", "YAWN", "YEAH", "YEAR", "YELL", "YOGA", "YOUR",
        "ZEAL", "ZERO", "ZINC", "ZONE", "ZOOM"
    };

    public KMAudio Audio;
    public KMBombModule Module;
    public KMSelectable[] Grid;
    public Text TemplateLetter;
    public Image TemplateWall;
    public Image TemplateStar;
    public Image TopDisplayRend;
    public Sprite[] WallSprites;

    private string mazeString; // 16 lower-case cipher letters (a, b, etc.)
    private int[] solution; // tile number, indexed by cipher letter
    private char[] cleartext;   // cleartext letters (uppercase) indexed by cipher letter
    private int currentTile = -1;   // tile number the user needs to click on
    private bool[] placedTiles;  // indexed by cipher letter

    private List<Text> allLetters = new List<Text>();
    private List<Image> allWalls = new List<Image>();
    private List<Image> allStars = new List<Image>();
    private const float GRID_PHYSICAL_WIDTH = 3f;
    private const float GRID_LETTER_SIZE = 0.02f;
    private const float GRID_LETTER_SIZE_SMALL = 0.01f;
    private const float GRID_TILE_SIZE = 1f;
    private const float GRID_STAR_SIZE = 0.8f;

    enum DisplayState { Letter, LetterAndWalls, Walls }
    private DisplayState[] displayStates = new DisplayState[16];

    // Logging
    private static int moduleIdCounter = 1;
    private int moduleId;
    private bool moduleSolved;
    private bool cannotPress = true;

    void Awake()
    {
        moduleId = moduleIdCounter++;

        for (var cell = 0; cell < Grid.Length; cell++)
            Grid[cell].OnInteract += CellPress(cell);
    }

    private static IEnumerable<bool?[][]> SolvePuzzle(bool?[][] known, string maze)
    {
        keepGoing:
        var anyDeduction = false;
        for (var i = 0; i < Deduction.AllDeductions.Length; i++)
        {
            var tup = Deduction.AllDeductions[i].Deduce(known, maze);
            if (tup != null)
            {
                anyDeduction = true;
                known[tup.Value.Letter][tup.Value.Direction] = tup.Value.IsWall;
            }
        }
        if (anyDeduction)
            goto keepGoing;

        var firstUnsolvedLtr = Array.FindIndex(known, walls => walls.Any(w => w == null));
        if (firstUnsolvedLtr == -1)
        {
            yield return known;
            yield break;
        }
        var firstUnsolvedDir = Enumerable.Range(0, 4).First(dir => known[firstUnsolvedLtr][dir] == null);
        foreach (var poss in new[] { true, false })
        {
            known[firstUnsolvedLtr][firstUnsolvedDir] = poss;
            foreach (var solution in SolvePuzzle(known.Select(k => k.ToArray()).ToArray(), maze))
                yield return solution;
        }
    }

    IEnumerable<FindWordResult> FindWords(string[] wordsSoFar, char?[] lettersSoFar, int row, string[] rows, string[][] eligibleWordsPerRow)
    {
        if (row == 4)
        {
            yield return new FindWordResult(wordsSoFar, lettersSoFar);
            yield break;
        }

        var wordOffset = Rnd.Range(0, eligibleWordsPerRow[row].Length);
        for (var wordIx = 0; wordIx < eligibleWordsPerRow[row].Length; wordIx++)
        {
            var word = eligibleWordsPerRow[row][(wordIx + wordOffset) % eligibleWordsPerRow[row].Length];

            // Check if ‘word’ satisfies the letters that have already been assigned
            for (var i = 0; i < 4; i++)
                if (lettersSoFar[rows[row][i] - 'a'] != null && word[i] != lettersSoFar[rows[row][i] - 'a'].Value)
                    goto busted;

            // Make sure that different cipher letters make different cleartext letters
            for (var i = 0; i < 4; i++)
                if (lettersSoFar[rows[row][i] - 'a'] == null && lettersSoFar.Contains(word[i]))
                    goto busted;

            var newLettersSoFar = lettersSoFar.ToArray();
            for (var i = 0; i < 4; i++)
                newLettersSoFar[rows[row][i] - 'a'] = word[i];

            foreach (var solution in FindWords(wordsSoFar.Append(word), newLettersSoFar, row + 1, rows, eligibleWordsPerRow))
                yield return solution;

            busted:;
        }
        foreach (var solution in FindWords(wordsSoFar.Append(null), lettersSoFar, row + 1, rows, eligibleWordsPerRow))
            yield return solution;
    }

    void Start()
    {
        tryAgain:
        mazeString = MazeGenerator.GenerateEncodedMaze();
        var solutions = SolvePuzzle(Enumerable.Range(0, mazeString.Distinct().Count()).Select(_ => new bool?[4]).ToArray(), mazeString).ToArray();
        var disambiguatorRequired = solutions.Length != 1;
        if (!disambiguatorRequired)
        {
            solution = solutions[0].Select(bs => (bs[0].Value ? 1 : 0) | (bs[1].Value ? 2 : 0) | (bs[2].Value ? 4 : 0) | (bs[3].Value ? 8 : 0)).ToArray();
            currentTile = solution.PickRandom();
        }
        else
        {
            var tileArrays = Enumerable.Range(0, 15).Select(tile => new bool?[] { (tile & 1) != 0, (tile & 2) != 0, (tile & 4) != 0, (tile & 8) != 0 }).ToArray();
            var tileCounts = tileArrays.Select(tile => solutions.Count(sol => sol.Any(t => t.SequenceEqual(tile)))).ToArray();
            var disambiguatingTiles = Enumerable.Range(0, 15).Where(tile => tileCounts[tile] == 1).ToArray();
            if (disambiguatingTiles.Length == 0)
                goto tryAgain;
            var disambiguator = disambiguatingTiles.PickRandom();

            var applicableSolution = solutions.Single(s => s.Any(t => t.SequenceEqual(tileArrays[disambiguator])));
            solution = applicableSolution.Select(bs => (bs[0].Value ? 1 : 0) | (bs[1].Value ? 2 : 0) | (bs[2].Value ? 4 : 0) | (bs[3].Value ? 8 : 0)).ToArray();

            currentTile = disambiguator;
        }

        // TODO: check if 2 deadends, what we have here doesn't work since it doesn't take into account the number of occurances of each letter in the maze.
        var deadends = new[] { 7, 11, 13, 14 };
        if (solution.Count(deadends.Contains) == 2)
            goto tryAgain;

        var rows = Enumerable.Range(0, 4).Select(r => mazeString.Substring(4 * r, 4)).ToArray();
        var eligibleWordsPerRow = rows.Select(row => _wordlist.Where(word =>
        {
            // example row = ‘abcd’
            // example word = ‘POLE’
            for (var i = 0; i < word.Length; i++)
                for (var j = i + 1; j < word.Length; j++)
                    if ((row[i] == row[j]) != (word[i] == word[j]))
                        return false;
            return true;
        }).ToArray()).ToArray();
        var maxPossible = eligibleWordsPerRow.Count(ews => ews.Length > 0);

        var maxSoFar = 0;
        string[] preferredSolution = null;
        foreach (var solution in FindWords(new string[0], new char?[mazeString.Distinct().Count()], 0, rows, eligibleWordsPerRow))
        {
            var numWords = solution.words.Count(s => s != null);
            if (numWords > maxSoFar)
            {
                preferredSolution = solution.words.Select((s, ix) =>
                {
                    if (s != null)
                        return s;
                    var nonWord = "";
                    for (var i = 0; i < 4; i++)
                    {
                        if (solution.letterAssoc[rows[ix][i] - 'a'] != null)
                            nonWord += solution.letterAssoc[rows[ix][i] - 'a'].Value;
                        else
                        {
                            var unassocLetter = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Where(ch => !solution.letterAssoc.Contains(ch)).PickRandom();
                            solution.letterAssoc[rows[ix][i] - 'a'] = unassocLetter;
                            nonWord += unassocLetter;
                        }
                    }
                    return nonWord;
                }).ToArray();
                cleartext = solution.letterAssoc.Select(c => c.Value).ToArray();
                maxSoFar = numWords;
            }
            if (numWords == maxPossible)
                break;
        }

        for (int v = 0; v < 16; v++)
        {
            var newLetter = Instantiate(TemplateLetter, TemplateLetter.transform.parent);
            newLetter.transform.localPosition = new Vector3(Mathf.Lerp(-GRID_PHYSICAL_WIDTH, GRID_PHYSICAL_WIDTH, (v % 4) / 3f), Mathf.Lerp(GRID_PHYSICAL_WIDTH, -GRID_PHYSICAL_WIDTH, (v / 4) / 3f));
            newLetter.text = preferredSolution[v / 4][v % 4].ToString();
            newLetter.transform.localScale = Vector3.zero;
            allLetters.Add(newLetter);
        }

        TemplateLetter.gameObject.SetActive(false);

        for (int v = 0; v < 16; v++)
        {
            var newWallPiece = Instantiate(TemplateWall, TemplateWall.transform.parent);
            newWallPiece.transform.localPosition = new Vector3(Mathf.Lerp(-GRID_PHYSICAL_WIDTH, GRID_PHYSICAL_WIDTH, (v % 4) / 3f), Mathf.Lerp(GRID_PHYSICAL_WIDTH, -GRID_PHYSICAL_WIDTH, (v / 4) / 3f));
            newWallPiece.color = Color.clear;
            allWalls.Add(newWallPiece);
        }

        TemplateWall.gameObject.SetActive(false);

        for (int v = 0; v < 16; v++)
        {
            var newStarPiece = Instantiate(TemplateStar, TemplateStar.transform.parent);
            newStarPiece.transform.localPosition = new Vector3(Mathf.Lerp(-GRID_PHYSICAL_WIDTH, GRID_PHYSICAL_WIDTH, (v % 4) / 3f), Mathf.Lerp(GRID_PHYSICAL_WIDTH, -GRID_PHYSICAL_WIDTH, (v / 4) / 3f));
            newStarPiece.transform.localScale = Vector3.zero;
            newStarPiece.color = new Color(1, 1, 1, 0.5f);
            allStars.Add(newStarPiece);
        }

        TemplateStar.gameObject.SetActive(false);

        TopDisplayRend.sprite = WallSprites[currentTile];
        placedTiles = new bool[cleartext.Length];

        Debug.LogFormat("[Literal Maze #{0}] Letters on module: {1}", moduleId, preferredSolution.Join(" "));
        Debug.LogFormat("<Literal Maze #{0}> Simplified maze: {1} / Disambiguator: {2}", moduleId, mazeString, disambiguatorRequired);
        Debug.LogFormat("[Literal Maze #{0}] Tiles: {1}", moduleId, solution.Join(","));

        Module.OnActivate += Activate;
    }

    private void Activate()
    {
        StartCoroutine(RunIntroAnim());
    }

    private IEnumerator RunIntroAnim(float duration = 0.5f, float intensity = 0.5f)
    {
        var initPositions = new List<Vector3>();
        for (int i = 0; i < 16; i++)
            initPositions.Add(allLetters[i].transform.localPosition);

        float timer = 0;
        while (timer < duration)
        {
            for (int i = 0; i < 16; i++)
            {
                allLetters[i].transform.localScale = Vector3.one * Easing.OutSine(timer, 0, GRID_LETTER_SIZE, duration);
                allLetters[i].color = new Color(1, 1, 1, Mathf.Lerp(0, 1, timer / duration));

                allLetters[i].transform.localPosition = new Vector3((Rnd.Range(-GRID_PHYSICAL_WIDTH, GRID_PHYSICAL_WIDTH) * (duration - timer) * intensity) + initPositions[i].x,
                    (Rnd.Range(GRID_PHYSICAL_WIDTH, -GRID_PHYSICAL_WIDTH) * (duration - timer) * intensity) + initPositions[i].y, 0);
            }
            yield return null;
            timer += Time.deltaTime;
        }
        for (int i = 0; i < 16; i++)
        {
            allLetters[i].transform.localScale = Vector3.one * GRID_LETTER_SIZE;
            allLetters[i].color = Color.white;

            allLetters[i].transform.localPosition = initPositions[i];
        }
        cannotPress = false;
    }

    private KMSelectable.OnInteractHandler CellPress(int cell)
    {
        return delegate
        {
            if (!cannotPress)
            {
                if (placedTiles[mazeString[cell] - 'a'])
                    return false;

                Grid[cell].AddInteractionPunch();

                if (solution[mazeString[cell] - 'a'] != currentTile)
                {
                    Debug.LogFormat("[Literal Maze #{0}] You clicked on cell {1} which has tile {2}, but we were looking for tile {3}. Strike!", moduleId, cell, solution[mazeString[cell] - 'a'], currentTile);
                    Module.HandleStrike();
                    Audio.PlaySoundAtTransform("strike", Grid[cell].transform);
                    return false;
                }

                Audio.PlaySoundAtTransform("whoosh", Grid[cell].transform);

                Debug.LogFormat("[Literal Maze #{0}] Cell {1} placed correctly with tile {2}.", moduleId, cell, currentTile);
                placedTiles[mazeString[cell] - 'a'] = true;

                var ltrs = Enumerable.Range(0, placedTiles.Length).Where(ix => !placedTiles[ix]).ToArray();
                if (ltrs.Length > 0)
                {
                    var ltr = ltrs.PickRandom();
                    currentTile = solution[ltr];
                    TopDisplayRend.sprite = WallSprites[currentTile];
                    Audio.PlaySoundAtTransform("correct", Grid[cell].transform);
                }
                else
                {
                    Debug.LogFormat("[Literal Maze #{0}] Module solved!", moduleId);
                    Module.HandlePass();
                    moduleSolved = true;
                    TopDisplayRend.sprite = WallSprites[16];
                    TopDisplayRend.color = new Color(1, 1, 1, 0.5f);
                    Audio.PlaySoundAtTransform("solve", TemplateLetter.transform);
                }

                for (var i = 0; i < 16; i++)
                {
                    allWalls[i].sprite = placedTiles[mazeString[i] - 'a'] ? WallSprites[solution[mazeString[i] - 'a']] : null;
                    SetDisplayState(i, moduleSolved ? DisplayState.Walls : placedTiles[mazeString[i] - 'a'] ? DisplayState.LetterAndWalls : DisplayState.Letter);
                }
            }
            return false;
        };
    }

    private void SetDisplayState(int cell, DisplayState newState)
    {
        if (displayStates[cell] == newState)
            return;

        switch (displayStates[cell])
        {
            case DisplayState.Letter:
                if (newState == DisplayState.LetterAndWalls)
                {
                    StartCoroutine(RevealWalls(allWalls[cell]));
                    StartCoroutine(SupressText(allLetters[cell]));
                }
                else if (newState == DisplayState.Walls)
                {
                    allWalls[cell].color = Color.white;
                    StartCoroutine(RevealWalls(allWalls[cell]));
                    StartCoroutine(HideText(allLetters[cell]));
                    StartCoroutine(RevealStar(allStars[cell]));
                }
                break;

            case DisplayState.LetterAndWalls:
                StartCoroutine(HideText(allLetters[cell]));
                StartCoroutine(RevealStar(allStars[cell]));
                break;

            case DisplayState.Walls:
                break;
        }

        displayStates[cell] = newState;
    }

    /*IEnumerator AnimateItem(Transform transform, float prevSize, float newSize)
    {
        var elapsed = 0f;
        const float duration = 1.1f;
        transform.gameObject.SetActive(true);
        while (elapsed < duration)
        {
            var t = Easing.InOutQuad(elapsed, prevSize, newSize, duration);
            transform.localScale = new Vector3(t, t, 1);
            yield return null;
            elapsed += Time.deltaTime;
        }
        transform.localScale = new Vector3(newSize, newSize, 1);
        if (newSize == 0)
            transform.gameObject.SetActive(false);
    }*/

    private IEnumerator RevealWalls(Image target, float duration = 0.5f, float angle = 500f)
    {
        target.color = Color.white;
        target.transform.localScale = Vector3.zero;

        float timer = 0;
        while (timer < duration)
        {
            target.transform.localScale = Vector3.one * Easing.OutSine(timer, 0, GRID_TILE_SIZE, duration);
            target.transform.localEulerAngles = Vector3.forward * Easing.OutSine(timer, angle, 0, duration);
            yield return null;
            timer += Time.deltaTime;
        }

        target.transform.localScale = Vector3.one * GRID_TILE_SIZE;
        target.transform.localEulerAngles = Vector3.zero;
    }

    private IEnumerator SupressText(Text target, float duration = 0.5f)
    {
        target.transform.localScale = Vector3.one * GRID_LETTER_SIZE;

        float timer = 0;
        while (timer < duration)
        {
            target.transform.localScale = Vector3.one * Easing.OutExpo(timer, GRID_LETTER_SIZE, GRID_LETTER_SIZE_SMALL, duration);
            yield return null;
            timer += Time.deltaTime;
        }

        target.transform.localScale = Vector3.one * GRID_LETTER_SIZE_SMALL;
    }

    private IEnumerator HideText(Text target, float duration = 0.5f)
    {
        target.transform.localScale = Vector3.one * GRID_LETTER_SIZE_SMALL;

        float timer = 0;
        while (timer < duration)
        {
            target.transform.localScale = Vector3.one * Easing.OutExpo(timer, GRID_LETTER_SIZE_SMALL, 0, duration);
            yield return null;
            timer += Time.deltaTime;
        }

        target.transform.localScale = Vector3.zero;
    }

    private IEnumerator RevealStar(Image target, float duration = 0.5f)
    {
        target.transform.localScale = Vector3.zero;

        float timer = 0;
        while (timer < duration)
        {
            target.transform.localScale = Vector3.one * Easing.OutSine(timer, 0, GRID_STAR_SIZE, duration);
            yield return null;
            timer += Time.deltaTime;
        }

        target.transform.localScale = Vector3.one * GRID_STAR_SIZE;
    }
}
