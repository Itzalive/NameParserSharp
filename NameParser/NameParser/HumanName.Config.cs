﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NameParser;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public partial class Piece
{
    /// <summary>
    /// Pieces that should join to their neighboring pieces, e.g. "and", "y" and "&". "of" and "the" are also include to facilitate joining multiple titles, e.g. "President of the United States".
    /// </summary>
    public static readonly HashSet<string> Conjunctions = new HashSet<string>
        { "&", "and", "et", "e", "of", "the", "und", "y" };

    /// <summary>
    /// Name pieces that appear before a last name. They join to the piece that follows them to make one new piece.
    /// </summary>
    public static readonly HashSet<string> Prefixes = new HashSet<string>
    {
        "abu",
        "al",
        "bin",
        "bon",
        "da",
        "dal",
        "de",
        "de\'",
        "degli",
        "dei",
        "del",
        "dela",
        "della",
        "delle",
        "delli",
        "dello",
        "der",
        "di",
        "dí",
        "do",
        "dos",
        "du",
        "ibn",
        "la",
        "le",
        "mac",
        "mc",
        "san",
        "santa",
        "st",
        "ste",
        "van",
        "vander",
        "vel",
        "von",
        "vom",
    };

    /// <summary>
    /// Pieces that come at the end of the name but are not last names. These potentially
    /// conflict with initials that might be at the end of the name.
    /// Post-nominal acronyms. Titles, degrees and other things people stick after their name
    /// that may or may not have periods between the letters. The parser removes periods
    /// when matching against these pieces.
    /// </summary>
    public static readonly HashSet<string> Suffixes = new HashSet<string>()
    {
        "(ret)",
        "(vet)",
        "8-vsb",
        "aas",
        "aba",
        "abc",
        "abd",
        "abpp",
        "abr",
        "aca",
        "acas",
        "ace",
        "acha",
        "acp",
        "ae",
        "ae",
        "aem",
        "afasma",
        "afc",
        "afc",
        "afm",
        "afm",
        "agsf",
        "aia",
        "aicp",
        "ala",
        "alc",
        "alp",
        "am",
        "amd",
        "ame",
        "amieee",
        "ams",
        "aphr",
        "apn aprn",
        "apr",
        "apss",
        "aqp",
        "arm",
        "arrc",
        "asa",
        "asc",
        "asid",
        "asla",
        "asp",
        "atc",
        "awb",
        "bca",
        "bcl",
        "bcss",
        "bds",
        "bem",
        "bem",
        "bls-i",
        "bpe",
        "bpi",
        "bpt",
        "bt",
        "btcs",
        "bts",
        "cacts",
        "cae",
        "caha",
        "caia",
        "cams",
        "cap",
        "capa",
        "capm",
        "capp",
        "caps",
        "caro",
        "cas",
        "casp",
        "cb",
        "cbe",
        "cbm",
        "cbne",
        "cbnt",
        "cbp",
        "cbrte",
        "cbs",
        "cbsp",
        "cbt",
        "cbte",
        "cbv",
        "cca",
        "ccc",
        "ccca",
        "cccm",
        "cce",
        "cchp",
        "ccie",
        "ccim",
        "cciso",
        "ccm",
        "ccmt",
        "ccna",
        "ccnp",
        "ccp",
        "ccp-c",
        "ccpr",
        "ccs",
        "ccufc",
        "cd",
        "cdal",
        "cdfm",
        "cdmp",
        "cds",
        "cdt",
        "cea",
        "ceas",
        "cebs",
        "ceds",
        "ceh",
        "cela",
        "cem",
        "cep",
        "cera",
        "cet",
        "cfa",
        "cfc",
        "cfcc",
        "cfce",
        "cfcm",
        "cfe",
        "cfeds",
        "cfi",
        "cfm",
        "cfp",
        "cfps",
        "cfr",
        "cfre",
        "cga",
        "cgap",
        "cgb",
        "cgc",
        "cgfm",
        "cgfo",
        "cgm",
        "cgm",
        "cgma",
        "cgp",
        "cgr",
        "cgsp",
        "ch",
        "ch",
        "cha",
        "chba",
        "chdm",
        "che",
        "ches",
        "chfc",
        "chfc",
        "chi",
        "chmc",
        "chmm",
        "chp",
        "chpa",
        "chpe",
        "chpln",
        "chpse",
        "chrm",
        "chsc",
        "chse",
        "chse-a",
        "chsos",
        "chss",
        "cht",
        "cia",
        "cic",
        "cie",
        "cig",
        "cip",
        "cipm",
        "cips",
        "ciro",
        "cisa",
        "cism",
        "cissp",
        "cla",
        "clsd",
        "cltd",
        "clu",
        "cm",
        "cma",
        "cmas",
        "cmc",
        "cmfo",
        "cmg",
        "cmp",
        "cms",
        "cmsp",
        "cmt",
        "cna",
        "cnm",
        "cnp",
        "cp",
        "cp-c",
        "cpa",
        "cpacc",
        "cpbe",
        "cpcm",
        "cpcu",
        "cpe",
        "cpfa",
        "cpfo",
        "cpg",
        "cph",
        "cpht",
        "cpim",
        "cpl",
        "cplp",
        "cpm",
        "cpo",
        "cpp",
        "cppm",
        "cprc",
        "cpre",
        "cprp",
        "cpsc",
        "cpsi",
        "cpss",
        "cpt",
        "cpwa",
        "crde",
        "crisc",
        "crma",
        "crme",
        "crna",
        "cro",
        "crp",
        "crt",
        "crtt",
        "csa",
        "csbe",
        "csc",
        "cscp",
        "cscu",
        "csep",
        "csi",
        "csm",
        "csp",
        "cspo",
        "csre",
        "csrte",
        "csslp",
        "cssm",
        "cst",
        "cste",
        "ctbs",
        "ctfa",
        "cto",
        "ctp",
        "cts",
        "cua",
        "cusp",
        "cva",
        "cva[22]",
        "cvo",
        "cvp",
        "cvrs",
        "cwap",
        "cwb",
        "cwdp",
        "cwep",
        "cwna",
        "cwne",
        "cwp",
        "cwsp",
        "cxa",
        "cyds",
        "cysa",
        "dabfm",
        "dabvlm",
        "dacvim",
        "dbe",
        "dc",
        "dcb",
        "dcm",
        "dcmg",
        "dcvo",
        "dd",
        "dds",
        "ded",
        "dep",
        "dfc",
        "dfm",
        "diplac",
        "diplom",
        "djur",
        "dma",
        "dmd",
        "dmin",
        "dnp",
        "do",
        "dpm",
        "dpt",
        "drb",
        "drmp",
        "drph",
        "dsc",
        "dsm",
        "dso",
        "dss",
        "dtr",
        "dvep",
        "dvm",
        "ea",
        "ed",
        "edd",
        "ei",
        "eit",
        "els",
        "emd",
        "emt-b",
        "emt-i/85",
        "emt-i/99",
        "emt-p",
        "enp",
        "erd",
        "esq",
        "evp",
        "faafp",
        "faan",
        "faap",
        "fac-c",
        "facc",
        "facd",
        "facem",
        "facep",
        "facha",
        "facofp",
        "facog",
        "facp",
        "facph",
        "facs",
        "faia",
        "faicp",
        "fala",
        "fashp",
        "fasid",
        "fasla",
        "fasma",
        "faspen",
        "fca",
        "fcas",
        "fcela",
        "fd",
        "fec",
        "fhames",
        "fic",
        "ficf",
        "fieee",
        "fmp",
        "fmva",
        "fnss",
        "fp&a",
        "fp-c",
        "fpc",
        "frm",
        "fsa",
        "fsdp",
        "fws",
        "gaee[14]",
        "gba",
        "gbe",
        "gc",
        "gcb",
        "gcb",
        "gchs",
        "gcie",
        "gcmg",
        "gcmg",
        "gcsi",
        "gcvo",
        "gcvo",
        "gisp",
        "git",
        "gm",
        "gmb",
        "gmr",
        "gphr",
        "gri",
        "grp",
        "gsmieee",
        "hccp",
        "hrs",
        "iaccp",
        "iaee",
        "iccm-d",
        "iccm-f",
        "idsm",
        "ifgict",
        "iom",
        "ipep",
        "ipm",
        "iso",
        "issp-csp",
        "issp-sa",
        "itil",
        "jd",
        "jp",
        "kbe",
        "kcb",
        "kchs/dchs",
        "kcie",
        "kcie",
        "kcmg",
        "kcsi",
        "kcsi",
        "kcvo",
        "kg",
        "khs/dhs",
        "kp",
        "kt",
        "lac",
        "lcmt",
        "lcpc",
        "lcsw",
        "leed ap",
        "lg",
        "litk",
        "litl",
        "litp",
        "llm",
        "lm",
        "lmsw",
        "lmt",
        "lp",
        "lpa",
        "lpc",
        "lpn",
        "lpss",
        "lsi",
        "lsit",
        "lt",
        "lvn",
        "lvo",
        "lvt",
        "ma",
        "maaa",
        "mai",
        "mba",
        "mbe",
        "mbs",
        "mc",
        "mcct",
        "mcdba",
        "mches",
        "mcm",
        "mcp",
        "mcpd",
        "mcsa",
        "mcsd",
        "mcse",
        "mct",
        "md",
        "mdiv",
        "mem",
        "mfa",
        "micp",
        "mieee",
        "mirm",
        "mle",
        "mls",
        "mlse",
        "mlt",
        "mm",
        "mmad",
        "mmas",
        "mnaa",
        "mnae",
        "mp",
        "mpa",
        "mph",
        "mpse",
        "mra",
        "ms",
        "msa",
        "msc",
        "mscmsm",
        "msm",
        "mt",
        "mts",
        "mvo",
        "nbc-his",
        "nbcch",
        "nbcch-ps",
        "nbcdch",
        "nbcdch-ps",
        "nbcfch",
        "nbcfch-ps",
        "nbct",
        "ncarb",
        "nccp",
        "ncidq",
        "ncps",
        "ncso",
        "ncto",
        "nd",
        "ndtr",
        "nicet i",
        "nicet ii",
        "nicet iii",
        "nicet iv",
        "nmd",
        "np",
        "np[18]",
        "nraemt",
        "nremr",
        "nremt",
        "nrp",
        "obe",
        "obi",
        "oca",
        "ocm",
        "ocp",
        "od",
        "om",
        "oscp",
        "ot",
        "pa-c",
        "pcc",
        "pci",
        "pe",
        "pfmp",
        "pg",
        "pgmp",
        "ph",
        "pharmd",
        "phc",
        "phd",
        "phr",
        "phrca",
        "pla",
        "pls",
        "pmc",
        "pmi-acp",
        "pmp",
        "pp",
        "pps",
        "prm",
        "psm i",
        "psm ii",
        "psm",
        "psp",
        "psyd",
        "pt",
        "pta",
        "qam",
        "qc",
        "qcsw",
        "qfsm",
        "qgm",
        "qpm",
        "qsd",
        "qsp",
        "ra",
        "rai",
        "rba",
        "rci",
        "rcp",
        "rd",
        "rdcs",
        "rdh",
        "rdms",
        "rdn",
        "res",
        "rfp",
        "rhca",
        "rid",
        "rls",
        "rmsks",
        "rn",
        "rp",
        "rpa",
        "rph",
        "rpl",
        "rrc",
        "rrt",
        "rrt-accs",
        "rrt-nps",
        "rrt-sds",
        "rtrp",
        "rvm",
        "rvt",
        "sa",
        "same",
        "sasm",
        "sccp",
        "scmp",
        "se",
        "secb",
        "sfp",
        "sgm",
        "shrm-cp",
        "shrm-scp",
        "si",
        "siie",
        "smieee",
        "sphr",
        "sra",
        "sscp",
        "stmieee",
        "tbr-ct",
        "td",
        "thd",
        "thm",
        "ud",
        "usa",
        "usaf",
        "usar",
        "uscg",
        "usmc",
        "usn",
        "usnr",
        "uxc",
        "uxmc",
        "vc",
        "vc",
        "vcp",
        "vd",
        "vrd",
    };

    public static readonly HashSet<string> SuffixesNotAcronyms = new HashSet<string>()
    {
        "dr",
        "esq",
        "esquire",
        "jr",
        "jnr",
        "junior",
        "sr",
        "snr",
        "2",
        "i",
        "ii",
        "iii",
        "iv",
        "v",
    };

    /// <summary>
    /// **Cannot include things that could also be first names**, e.g. "dean".
    /// Many of these from wikipedia: https://en.wikipedia.org/wiki/Title.
    /// The parser recognizes chains of these including conjunctions allowing
    /// recognition titles like "Deputy Secretary of State".
    /// </summary>
    public static readonly HashSet<string> Titles = new HashSet<string>
    {
        // <FirstNameTitles>
        "aunt",
        "auntie",
        "brother",
        "dame",
        "father",
        "king",
        "maid",
        "master",
        "mother",
        "pope",
        "queen",
        "sir",
        "sister",
        "uncle",
        "sheikh",
        "sheik",
        "shaik",
        "shayk",
        "shaykh",
        "shaikh",
        "cheikh",
        "shekh",
        // </FirstNameTitles>
        "attaché",
        "chargé d'affaires",
        "king's",
        "marchioness",
        "marquess",
        "marquis",
        "marquise",
        "queen's",
        "10th",
        "1lt",
        "1sgt",
        "1st",
        "1stlt",
        "1stsgt",
        "2lt",
        "2nd",
        "2ndlt",
        "3rd",
        "4th",
        "5th",
        "6th",
        "7th",
        "8th",
        "9th",
        "a1c",
        "ab",
        "abbess",
        "abbot",
        "abolitionist",
        "academic",
        "acolyte",
        "activist",
        "actor ",
        "actress",
        "adept",
        "adjutant",
        "adm",
        "admiral",
        "advertising",
        "adviser",
        "advocate",
        "air",
        "akhoond",
        "alderman",
        "almoner",
        "ambassador",
        "amn",
        "analytics",
        "anarchist",
        "animator",
        "anthropologist",
        "appellate",
        "apprentice",
        "arbitrator",
        "archbishop",
        "archdeacon",
        "archdruid",
        "archduchess",
        "archduke",
        "archeologist",
        "architect",
        "arhat",
        "army",
        "arranger",
        "assistant",
        "assoc",
        "associate",
        "asst",
        "astronomer",
        "attache",
        "attorney",
        "author",
        "award-winning",
        "ayatollah",
        "baba",
        "bailiff",
        "ballet",
        "bandleader",
        "banker",
        "banner",
        "bard",
        "baron",
        "baroness",
        "barrister",
        "baseball",
        "bearer",
        "behavioral",
        "bench",
        "bg",
        "bgen",
        "biblical",
        "bibliographer",
        "biochemist",
        "biographer",
        "biologist",
        "bishop",
        "blessed",
        "blogger",
        "blues",
        "bodhisattva",
        "bookseller",
        "botanist",
        "bp",
        "brigadier",
        "briggen",
        "british",
        "broadcaster",
        "buddha",
        "burgess",
        "burlesque",
        "business",
        "businessman",
        "businesswoman",
        "bwana",
        "canon",
        "capt",
        "captain",
        "cardinal",
        "cartographer",
        "cartoonist",
        "catholicos",
        "ccmsgt",
        "cdr",
        "celebrity",
        "ceo",
        "cfo",
        "chair",
        "chairs",
        "chancellor",
        "chaplain",
        "chef",
        "chemist",
        "chief",
        "chieftain",
        "choreographer",
        "civil",
        "classical",
        "clergyman",
        "clerk",
        "cmsaf",
        "cmsgt",
        "co-chair",
        "co-chairs",
        "co-founder",
        "coach",
        "col",
        "collector",
        "colonel",
        "comedian",
        "comedienne",
        "comic",
        "commander",
        "commander-in-chief",
        "commodore",
        "composer",
        "compositeur",
        "comptroller",
        "computer",
        "comtesse",
        "conductor",
        "consultant",
        "controller",
        "corporal",
        "corporate",
        "correspondent",
        "councillor",
        "counselor",
        "count",
        "countess",
        "courtier",
        "cpl",
        "cpo",
        "cpt",
        "credit",
        "criminal",
        "criminologist",
        "critic",
        "csm",
        "curator",
        "customs",
        "cwo-2",
        "cwo-3",
        "cwo-4",
        "cwo-5",
        "cwo2",
        "cwo3",
        "cwo4",
        "cwo5",
        "cyclist",
        "dancer",
        "dcn",
        "deacon",
        "delegate",
        "deputy",
        "designated",
        "designer",
        "detective",
        "developer",
        "diplomat",
        "dir",
        "director",
        "discovery",
        "dissident",
        "district",
        "division",
        "do",
        "docent",
        "docket",
        "doctor",
        "doyen",
        "dpty",
        "dr",
        "dra",
        "dramatist",
        "druid",
        "drummer",
        "duchesse",
        // "duke", // a common first name
        "dutchess",
        "ecologist",
        "economist",
        "editor",
        "edmi",
        "edohen",
        "educator",
        "effendi",
        "ekegbian",
        "elerunwon",
        "eminence",
        "emperor",
        "empress",
        "engineer",
        "english",
        "ens",
        "entertainer",
        "entrepreneur",
        "envoy",
        "essayist",
        "evangelist",
        "excellency",
        "excellent",
        "exec",
        "executive",
        "expert",
        "fadm",
        "family",
        "federal",
        "field",
        "film",
        "financial",
        "first",
        "flag",
        "flying",
        "foreign",
        "forester",
        "founder",
        "fr",
        "friar",
        "gaf",
        "gen",
        "general",
        "generalissimo",
        "gentiluomo",
        "giani",
        "goodman",
        "goodwife",
        "governor",
        "graf",
        "grand",
        "group",
        "guitarist",
        "guru",
        "gyani",
        "gysgt",
        "hajji",
        "headman",
        "heir",
        "heiress",
        "her",
        "hereditary",
        "high",
        "highness",
        "his",
        "historian",
        "historicus",
        "historien",
        "holiness",
        "hon", // sorry Hon Solo, but judges seem more common.
        "honorable",
        "honourable",
        "host",
        "illustrator",
        "imam",
        "industrialist",
        "information",
        "instructor",
        "intelligence",
        "intendant",
        "inventor",
        "investigator",
        "investor",
        "journalist",
        "journeyman",
        "jr",
        "judge",
        "judicial",
        "junior",
        "jurist",
        "keyboardist",
        "kingdom",
        "knowledge",
        "lady",
        "lama",
        "lamido",
        "law",
        "lawyer",
        "lcdr",
        "lcpl",
        "leader",
        "lecturer",
        "legal",
        "librarian",
        "lieutenant",
        "linguist",
        "literary",
        "lord",
        "lt",
        "ltc",
        "ltcol",
        "ltg",
        "ltgen",
        "ltjg",
        "lyricist",
        "madam",
        "madame",
        "mademoiselle",
        "mag",
        "mag-judge",
        "mag/judge",
        "magistrate",
        "magistrate-judge",
        "magnate",
        "maharajah",
        "maharani",
        "mahdi",
        "maj",
        "majesty",
        "majgen",
        "manager",
        "marcher",
        "marchess",
        "marketing",
        "marquis",
        "mathematician",
        "mathematics",
        "matriarch",
        "mayor",
        "mcpo",
        "mcpoc",
        "mcpon",
        "md",
        "member",
        "memoirist",
        "merchant",
        "met",
        "metropolitan",
        "mg",
        "mgr",
        "mgysgt",
        "military",
        "minister",
        "miss",
        "misses",
        "missionary",
        "mister",
        "mlle",
        "mme",
        "mobster",
        "model",
        "monk",
        "monsignor",
        "most",
        "mountaineer",
        "mpco-cg",
        "mr",
        "mrs",
        "ms",
        "msg",
        "msgt",
        "mufti",
        "mullah",
        "municipal",
        "murshid",
        "musician",
        "musicologist",
        "mx",
        "mystery",
        "nanny",
        "narrator",
        "national",
        "naturalist",
        "navy",
        "neuroscientist",
        "novelist",
        "nurse",
        "obstetritian",
        "officer",
        "opera",
        "operating",
        "ornithologist",
        "painter",
        "paleontologist",
        "pastor",
        "patriarch",
        "pediatrician",
        "personality",
        "petty",
        "pfc",
        "pharaoh",
        "phd",
        "philantropist",
        "philosopher",
        "photographer",
        "physician",
        "physicist",
        "pianist",
        "pilot",
        "pioneer",
        "pir",
        "player",
        "playwright",
        "po1",
        "po2",
        "po3",
        "poet",
        "police",
        "political",
        "politician",
        "prefect",
        "prelate",
        "premier",
        "pres",
        "presbyter",
        "president",
        "presiding",
        "priest",
        "priestess",
        "primate",
        "prime",
        "prin",
        "prince",
        "princess",
        "principal",
        "printer",
        "printmaker",
        "prior",
        "private",
        "pro",
        "producer",
        "prof",
        "professor",
        "provost",
        "pslc",
        "psychiatrist",
        "psychologist",
        "publisher",
        "pursuivant",
        "pv2",
        "pvt",
        "rabbi",
        "radio",
        "radm",
        "rangatira",
        "ranger",
        "rdml",
        "rear",
        "rebbe",
        "registrar",
        "rep",
        "representative",
        "researcher",
        "resident",
        "rev",
        "revenue",
        "reverend",
        "right",
        "risk",
        "rock",
        "royal",
        "rt",
        "sa",
        "sailor",
        "saint",
        "sainte",
        "saoshyant",
        "satirist",
        "scholar",
        "schoolmaster",
        "scientist",
        "scpo",
        "screenwriter",
        "se",
        "secretary",
        "security",
        "seigneur",
        "senator",
        "senior",
        "senior-judge",
        "sergeant",
        "servant",
        "sfc",
        "sgm",
        "sgt",
        "sgtmaj",
        "sgtmajmc",
        "shehu",
        "sheikh",
        "sheriff",
        "siddha",
        "singer",
        "singer-songwriter",
        "sma",
        "smsgt",
        "sn",
        "soccer",
        "social",
        "sociologist",
        "software",
        "soldier",
        "solicitor",
        "soprano",
        "spc",
        "speaker",
        "special",
        "sr",
        "sra",
        "srta",
        "ssg",
        "ssgt",
        "st",
        "staff",
        "state",
        "states",
        "strategy",
        "subaltern",
        "subedar",
        "suffragist",
        "sultan",
        "sultana",
        "superior",
        "supreme",
        "surgeon",
        "swami",
        "swordbearer",
        "sysselmann",
        "tax",
        "teacher",
        "technical",
        "technologist",
        "television ",
        "tenor",
        "theater",
        "theatre",
        "theologian",
        "theorist",
        "timi",
        "tirthankar",
        "translator",
        "travel",
        "treasurer",
        "tsar",
        "tsarina",
        "tsgt",
        "uk",
        "united",
        "us",
        "vadm",
        "vardapet",
        "vc",
        "venerable",
        "verderer",
        "vicar",
        "vice",
        "viscount",
        "vizier",
        "vocalist",
        "voice",
        "warden",
        "warrant",
        "wing",
        "wm",
        "wo-1",
        "wo1",
        "wo2",
        "wo3",
        "wo4",
        "wo5",
        "woodman",
        "writer",
        "zoologist",
    };

    private static readonly HashSet<string> RomanNumerals = new HashSet<string>
    {
        "i",
        "ii",
        "iii",
        "iv",
        "v",
        "vi",
        "vii",
        "viii",
        "ix",
        "x",
        "xi",
        "xiii",
        "xiv",
        "xv",
        "xvi",
        "xvii",
        "xviii",
        "xix",
        "xx"
    };
}

public partial class HumanName
{
    /// <summary>
    /// Any pieces that are not capitalized by capitalizing the first letter.
    /// </summary>
    public static readonly ISet<Tuple<string, ReadOnlyMemory<char>>> CapitalizationExceptions = new HashSet<Tuple<string, ReadOnlyMemory<char>>>
    {
        Tuple.Create("ii", "II".AsMemory()),
        Tuple.Create("iii", "III".AsMemory()),
        Tuple.Create("iv", "IV".AsMemory()),
        Tuple.Create("md", "M.D.".AsMemory()),
        Tuple.Create("phd", "Ph.D.".AsMemory())
    };

    ///<summary>
    /// When these titles appear with a single other name, that name is a first name, e.g.
    /// "Sir John", "Sister Mary", "Queen Elizabeth".
    /// </summary>
    private static readonly HashSet<string> FirstNameTitles = new HashSet<string>
    {
        "aunt",
        "auntie",
        "brother",
        "dame",
        "father",
        "king",
        "maid",
        "master",
        "mother",
        "pope",
        "queen",
        "sir",
        "sister",
        "uncle",
        "sheikh",
        "sheik",
        "shaik",
        "shayk",
        "shaykh",
        "shaikh",
        "cheikh",
        "shekh",
    };

    private static readonly Regex RegexSpaces = new Regex(@"\s+", RegexOptions.None, TimeSpan.FromMilliseconds(100));

    private static readonly Regex RegexWord = new Regex("(\\w|\\.)+", RegexOptions.None, TimeSpan.FromMilliseconds(100));

    /// <summary>
    /// For handling names that start with Mc or Mac such as McBride, MacDonald
    /// </summary>
    private static readonly Regex RegexMac =
        new Regex(@"^(ma?c)(\w{2,})", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));

    private static readonly Regex RegexQuotedWord = new Regex(@"(?<!\w)\'([^\s]*?)\'(?!\w)",
        RegexOptions.None, TimeSpan.FromMilliseconds(100));

    private static readonly Regex RegexDoubleQuotes = new Regex(@"\""(.*?)\""",
        RegexOptions.None, TimeSpan.FromMilliseconds(100));

    private static readonly Regex RegexParenthesis = new Regex(@"\((.*?)\)",
        RegexOptions.None, TimeSpan.FromMilliseconds(100));

    private static readonly Regex RegexPhd =
        new Regex(@"\s(ph\.?\s+d\.?)", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(100));
}