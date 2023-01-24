namespace WaveFunction.MagicSystemSketch
{
    public enum Aspect
    {
        Ignis, //hot
        Tepidus, //tepid
        Frigus, // cold
        Fortis, // strong
        Medius, //normal
        Debilis, // weak
        Discordia, // chaos
        Tractus, // Complex
        Concordia, // order
        Lux, // bright
        Ambiens, //ambient
        Umbra, // shadow
        Sylva, // Wild
        Innatus, // Nature
        Aridus, // Cultivated
        Gravis, // Heavy
        Levis, // light
        Bonum, // Good
        Pensare, // Balanced
        Malum, // Evil
        Velox, // Fast
        Tardus, // Slow
        Quietus // Frozen
    }
    
    // Potentia // Power (weak-strong)
    // Low: Fragilis, Tenuis, Exilis
    // Medium: Medius, Medianus, Modicus
    // High: Robustus, Fortis, Potens
    //
    // Flexum // Flexibility (rigid-fluid)
    // Low: Durus, Solidus, Compactus
    // Medium: Elasticus, Fluidus, Mobilus
    // High: Placabilis, Mollis, Mutabilis
    //
    // Nixus // Opposition (neutral-opposed)
    // Low: Pacis, Concordia, Amicitia
    // Medium: Discordia, Rixa, Contentio
    // High: Discordans, Inimicus, Contrarius
    //
    // Umbra // Shadow (bright-dark)
    // Low: Clarus, Lucidus, Splendidus
    // Medium: Tenebrosus, Obscurus, Caliginosus
    // High: Vultus, Nubilus, Tenebrificus
    //
    // Furor // Fury (calm-frenzied)
    // Low: Tranquillus, Quietus, Pacatus
    // Medium: Excitatus, Vigorosus, Celer
    // High: Furens, Rabidus, Impetuosus
    //
    // Sensus // Perception (insensible-sensitive)
    // Low: Immotus, Insensibilis, Invisibilis
    // Medium: Perceptibilis, Sensibilis, Observabilis
    // High: Acutus, Perspicax, Acumen
    //
    // Vigor // Vigor (feeble-vigorous)
    // Low: Debilis, Infirmus, Extenuatus
    // Medium: Valens, Robustus, Strenuus
    // High: Viridis, Erectus, Vigorosus
    //
    // Tempestas // Storm (calm-tempestuous)
    // Low: Placidus, Lenis, Tranquillus
    // Medium: Turbulentus, Vexatus, Procellosus
    // High: Tempestuosus, Furiosus, Procella

    public enum Element
    {
        Solidum, // solidity (air-rock)
        Calor, // temperature (water-fire)
        Entropia, // orderedness (entropy-order)
        Lumines, // luminance (dark-light)
        Natura, // Naturalness (technological-natural)
        Densitas, // Density (heavy-light)
        Harmonius, // Balance (harmful-helpful)
        Motus // Motion (nearby-distant)
    }

    public static class EleAspects
    {
        public static Aspect Positive(this Element e)
        {
            Dictionary<Element, Aspect> conversion = new Dictionary<Element, Aspect>()
            {
                { Element.Solidum, Aspect.Fortis },
                { Element.Calor, Aspect.Ignis },
                { Element.Entropia, Aspect.Concordia },
                { Element.Lumines, Aspect.Lux },
                { Element.Natura, Aspect.Sylva },
                { Element.Densitas, Aspect.Gravis },
                { Element.Harmonius, Aspect.Bonum },
                { Element.Motus, Aspect.Tardus }
            };
            return conversion[e];
        }

        public static Aspect Negative(this Element e)
        {
            Dictionary<Element, Aspect> conversion = new Dictionary<Element, Aspect>()
            {
                { Element.Solidum, Aspect.Debilis },
                { Element.Calor, Aspect.Frigus },
                { Element.Entropia, Aspect.Discordia },
                { Element.Lumines, Aspect.Umbra },
                { Element.Natura, Aspect.Aridus },
                { Element.Densitas, Aspect.Levis },
                { Element.Harmonius, Aspect.Malum },
                { Element.Motus, Aspect.Velox }
            };
            return conversion[e];
        }

        public static Element IsElement(this Aspect a)
        {
            Dictionary<Aspect, Element> conversion = new Dictionary<Aspect, Element>()
            {
                { Aspect.Debilis, Element.Solidum },
                { Aspect.Frigus, Element.Calor },
                { Aspect.Discordia, Element.Entropia },
                { Aspect.Umbra, Element.Lumines },
                { Aspect.Aridus, Element.Natura },
                { Aspect.Levis, Element.Densitas },
                { Aspect.Malum, Element.Harmonius },
                { Aspect.Velox, Element.Motus },

                { Aspect.Fortis, Element.Solidum },
                { Aspect.Ignis, Element.Calor },
                { Aspect.Concordia, Element.Entropia },
                { Aspect.Lux, Element.Lumines },
                { Aspect.Sylva, Element.Natura },
                { Aspect.Gravis, Element.Densitas },
                { Aspect.Bonum, Element.Harmonius },
                { Aspect.Tardus, Element.Motus }
            };
            return conversion[a];
        }
    }
}
