using System.Collections.Generic;

namespace define
{
    public class Metadata
    {
        public string provider { get; set; }
    }

    public class Example
    {
        public string text { get; set; }
    }

    public class Subsens
    {
        public List<string> definitions { get; set; }
        public List<Example> examples { get; set; }
        public string id { get; set; }
        public List<string> regions { get; set; }
    }

    public class Sens
    {
        public List<string> definitions { get; set; }
        public List<Example> examples { get; set; }
        public string id { get; set; }
        public List<Subsens> subsenses { get; set; }
    }

    public class VariantForm
    {
        public string text { get; set; }
    }

    public class GrammaticalFeature
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Entry
    {
        public List<string> etymologies { get; set; }
        public string homographNumber { get; set; }
        public List<Sens> senses { get; set; }
        public List<VariantForm> variantForms { get; set; }
        public List<GrammaticalFeature> grammaticalFeatures { get; set; }
    }

    public class Pronunciation
    {
        public List<string> dialects { get; set; }
        public string phoneticNotation { get; set; }
        public string phoneticSpelling { get; set; }
        public string audioFile { get; set; }
    }

    public class LexicalEntry
    {
        public List<Entry> entries { get; set; }
        public string language { get; set; }
        public string lexicalCategory { get; set; }
        public List<Pronunciation> pronunciations { get; set; }
        public string text { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string language { get; set; }
        public List<LexicalEntry> lexicalEntries { get; set; }
        public string type { get; set; }
        public string word { get; set; }
    }

    public class DictionaryApiResult
    {
        public Metadata metadata { get; set; }
        public List<Result> results { get; set; }
    }
}
