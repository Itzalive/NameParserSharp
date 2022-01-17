﻿namespace NameParser
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public partial class HumanName
    {
        /// <summary>
        /// Any pieces that are not capitalized by capitalizing the first letter.
        /// </summary>
        public static readonly ISet<Tuple<string, string>> CapitalizationExceptions = new HashSet<Tuple<string, string>>
        {
            Tuple.Create("ii", "II"),
            Tuple.Create("iii", "III"),
            Tuple.Create("iv", "IV"),
            Tuple.Create("md", "M.D."),
            Tuple.Create("phd", "Ph.D.")
        };

        /// <summary>
        /// Pieces that should join to their neighboring pieces, e.g. "and", "y" and "&". "of" and "the" are also include to facilitate joining multiple titles, e.g. "President of the United States".
        /// </summary>
        public static readonly ISet<string> Conjunctions = new HashSet<string> { "&", "and", "et", "e", "of", "the", "und", "y" };


        /// <summary>
        /// Name pieces that appear before a last name. They join to the piece that follows them to make one new piece.
        /// </summary>
        public static readonly ISet<string> Prefixes = new HashSet<string> 
        {
            "abu", "bon", "bin", "da", "dal", "de", "del", "dem", "den", "der", "de", "di", "dí", "het", "ibn", "in", "la", "le", "onder", "op", "san", "santa", "st", "ste", "'t", "ten", "van", "vel", "von"
        };

        /// <summary>
        /// For handling names that start with Mc or Mac such as McBride, MacDonald
        /// </summary>
        private static readonly Regex RegexMac = new Regex(@"^(ma?c)(\w+)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Pieces that come at the end of the name but are not last names. These potentially
        /// conflict with initials that might be at the end of the name.

        /// These may be updated in the future because some of them are actually titles that just
        /// come at the end of the name, so semantically this is wrong. Positionally, it's correct.
        /// </summary>
        public static readonly ISet<string> Suffixes = new HashSet<string>()
        {
            "esq",
            "esquire",
            "jr",
            "jnr",
            "sr",
            "snr",
            "2",
            "i",
            "ii",
            "iii",
            "iv",
            "v",
            "clu",
            "chfc",
            "cfp",
            "cpa",
            "csm",
            "do",
            "dds",
            "dpm",
            "dmd",
            "md",
            "mba",
            "ma",
            "phd",
            "phr",
            "pmp",
            "mp",
            "qc"
        };

        ///<summary>
        /// When these titles appear with a single other name, that name is a first name, e.g.
        /// "Sir John", "Sister Mary", "Queen Elizabeth".
        /// </summary>
        public static readonly ISet<string> FirstNameTitles = new HashSet<string>
        {
            "sir",
            "dame",
            "king",
            "queen",
            "master",
            "maid",
            "uncle",
            "auntie",
            "aunt",
            "brother",
            "sister",
            "mother",
            "father",
            "pope"
        };


        /// <summary>
        /// **Cannot include things that could also be first names**, e.g. "dean".
        /// Many of these from wikipedia: https://en.wikipedia.org/wiki/Title.
        /// The parser recognizes chains of these including conjunctions allowing 
        /// recognition titles like "Deputy Secretary of State".
        /// </summary>
        public static readonly ISet<string> Titles = new HashSet<string>
        {
            // <FirstNameTitles>
            "sir",
            "dame",
            "king",
            "queen",
            "master",
            "maid",
            "uncle",
            "auntie",
            "aunt",
            "brother",
            "sister",
            "mother",
            "father",
            "pope",
            // </FirstNameTitles>
            "dr",
            "doctor",
            "miss",
            "misses",
            "mr",
            "mister",
            "mrs",
            "ms",
            "rev",
            "madam",
            "madame",
            "ab",
            "2ndlt",
            "amn",
            "1stlt",
            "a1c",
            "capt",
            "sra",
            "maj",
            "ssgt",
            "ltcol",
            "tsgt",
            "col",
            "briggen",
            "1stsgt",
            "majgen",
            "smsgt",
            "ltgen",
            "cmsgt",
            "ccmsgt",
            "cmsaf",
            "pvt",
            "2lt",
            "pv2",
            "1lt",
            "pfc",
            "cpt",
            "spc",
            "cpl",
            "ltc",
            "sgt",
            "ssg",
            "bg",
            "sfc",
            "mg",
            "msg",
            "ltg",
            "1sgt",
            "sgm",
            "csm",
            "sma",
            "wo1",
            "wo2",
            "wo3",
            "wo4",
            "wo5",
            "ens",
            "sa",
            "ltjg",
            "sn",
            "lt",
            "po3",
            "lcdr",
            "po1",
            "po2",
            "cdr",
            "cpo",
            "scpo",
            "mcpo",
            "vadm",
            "mcpoc",
            "adm",
            "mpco-cg",
            "lcpl",
            "gysgt",
            "bgen",
            "msgt",
            "mgysgt",
            "gen",
            "sgtmaj",
            "sgtmajmc",
            "wo-1",
            "cwo-2",
            "cwo-3",
            "cwo-4",
            "cwo-5",
            "rdml",
            "radm",
            "mcpon",
            "fadm",
            "cwo2",
            "cwo3",
            "cwo4",
            "cwo5",
            "rt",
            "lord",
            "lady",
            "duke",
            "dutchess",
            "representative",
            "rep",
            "senator",
            "cardinal",
            "secretary",
            "state",
            "foreign",
            "minister",
            "speaker",
            "president",
            "pres",
            "ceo",
            "cfo",
            "deputy",
            "dpty",
            "executive",
            "exec",
            "vice",
            "vc",
            "councillor",
            "manager",
            "mgr",
            "alderman",
            "delegate",
            "mayor",
            "lieutenant",
            "governor",
            "prefect",
            "prelate",
            "premier",
            "burgess",
            "ambassador",
            "envoy",
            "attaché",
            "chargé d'affaires",
            "provost",
            "marquis",
            "marquess",
            "marquise",
            "marchioness",
            "archduke",
            "archduchess",
            "viscount",
            "baron",
            "emperor",
            "empress",
            "tsar",
            "tsarina",
            "leader",
            "abbess",
            "abbot",
            "friar",
            "superior",
            "reverend",
            "bishop",
            "archbishop",
            "metropolitan",
            "presbyter",
            "priest",
            "priestess",
            "matriarch",
            "patriarch",
            "catholicos",
            "vicar",
            "chaplain",
            "canon",
            "pastor",
            "primate",
            "servant",
            "venerable",
            "blessed",
            "saint",
            "member",
            "solicitor",
            "mufti",
            "grand",
            "chancellor",
            "barrister",
            "bailiff",
            "attorney",
            "advocate",
            "deacon",
            "archdeacon",
            "acolyte",
            "elder",
            "monsignor",
            "almoner",
            "prof",
            "colonel",
            "general",
            "commodore",
            "air",
            "corporal",
            "staff",
            "chief",
            "first",
            "sergeant",
            "admiral",
            "high",
            "rear",
            "brigadier",
            "captain",
            "group",
            "commander",
            "commander-in-chief",
            "wing",
            "adjutant",
            "director",
            "dir",
            "generalissimo",
            "resident",
            "surgeon",
            "officer",
            "controller",
            "academic",
            "analytics",
            "business",
            "credit",
            "financial",
            "information",
            "security",
            "knowledge",
            "marketing",
            "operating",
            "petty",
            "risk",
            "strategy",
            "technical",
            "warrant",
            "corporate",
            "customs",
            "field",
            "flag",
            "flying",
            "intelligence",
            "pilot",
            "police",
            "political",
            "revenue",
            "senior",
            "sr",
            "junior",
            "jr",
            "private",
            "principal",
            "prin",
            "coach",
            "nurse",
            "nanny",
            "docent",
            "lama",
            "druid",
            "archdruid",
            "rabbi",
            "rebbe",
            "buddha",
            "ayatollah",
            "imam",
            "bodhisattva",
            "mullah",
            "mahdi",
            "saoshyant",
            "tirthankar",
            "vardapet",
            "pharaoh",
            "sultan",
            "sultana",
            "maharajah",
            "maharani",
            "vizier",
            "chieftain",
            "comptroller",
            "courtier",
            "curator",
            "doyen",
            "edohen",
            "ekegbian",
            "elerunwon",
            "forester",
            "gentiluomo",
            "headman",
            "intendant",
            "lamido",
            "marcher",
            "prior",
            "pursuivant",
            "rangatira",
            "ranger",
            "registrar",
            "seigneur",
            "shehu",
            "sheikh",
            "sheriff",
            "subaltern",
            "subedar",
            "sysselmann",
            "timi",
            "treasurer",
            "verderer",
            "warden",
            "hereditary",
            "woodman",
            "bearer",
            "banner",
            "swordbearer",
            "apprentice",
            "journeyman",
            "adept",
            "akhoond",
            "arhat",
            "bwana",
            "goodman",
            "goodwife",
            "bard",
            "hajji",
            "baba",
            "effendi",
            "giani",
            "gyani",
            "guru",
            "siddha",
            "pir",
            "murshid",
            "attache",
            "prime",
            "united",
            "states",
            "national",
            "associate",
            "assoc",
            "assistant",
            "asst",
            "supreme",
            "appellate",
            "judicial",
            "queen's",
            "king's",
            "prince",
            "princess",
            "bench",
            "right",
            "majesty",
            "his",
            "her",
            "kingdom",
            "royal",
            "honorable",
            "honourable",
            "hon",
            "magistrate",
            "mag",
            "judge",
            "designated",
            "us",
            "uk",
            "federal",
            "district",
            "arbitrator",
            "pro",
            "se",
            "law",
            "clerk",
            "docket",
            "pslc",
            "special",
            "municipal",
            "tax",
            "civil",
            "criminal",
            "family",
            "presiding",
            "division",
            "edmi",
            "discovery",
            "magistrate-judge",
            "mag-judge",
            "senior-judge",
            "mag/judge"
        };
    }
}