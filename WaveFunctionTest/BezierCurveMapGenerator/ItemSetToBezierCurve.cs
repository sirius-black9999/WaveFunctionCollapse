using System;
using NUnit.Framework;
using WaveFunction.ARPG.Items;
using WaveFunction.ARPG.Worldgen;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;
using WaveFunctionTest.PropertyTesting.Generators;

namespace WaveFunctionTest.BezierCurveMapGenerator
{
    public static class ItemSetToBezierCurve
    {
        private static readonly Element[] elements = new[]
        {
            Element.Solidum,
            Element.Calor,
            Element.Densitas,
            Element.Entropia,
            Element.Harmonius,
            Element.Lumines,
            Element.Motus,
            Element.Natura
        };

        [Test]
        public static void Worldgen_Key_Has_Three_Slots_Accepting_Items(
            [Range(0, 7)] int e)
        {
            //Arrange

            var GeneratorKey = new WorldgenPiece
            {
                PointSlot = ItemStore.Boots.Done,
                Next = new WorldgenPiece()
                {
                    PointSlot = ItemStore.Gloves.Done,
                    Next = new WorldgenPiece()
                    {
                        PointSlot = ItemStore.Ring.Done,
                    }
                }
            };
            //Assert
            var result = GeneratorKey.ResultingCurve();
            Assert.That(result[0].Point[elements[e]], Is.EqualTo(0));
            Assert.That(result[1].Point[elements[e]], Is.EqualTo(0));
            Assert.That(result[2].Point[elements[e]], Is.EqualTo(0));
        }

        [Test]
        public static void Worldgen_Key_Slots_May_Be_Altered(
            [Range(0, 7)] int e)
        {
            //Arrange
            
            var GeneratorKey =WorldgenPiece.Make.Add(ItemStore.Boots.WithElement(Element.Calor, 10).Done)
                .Add(ItemStore.Gloves.WithElement(Element.Harmonius, -1).Done)
                .Add(ItemStore.Ring.WithElement(Element.Densitas, 2).Done).Finish();
            //Act
            var result = GeneratorKey.ResultingCurve();
            //Assert
            Assert.That(result[0].Point[elements[e]],
                Is.EqualTo((e == 1) ? 2.767 : 0).Within(0.001));
            Assert.That(result[1].Point[elements[e]],
                Is.EqualTo((e == 4) ? -1 : 0).Within(0.1));
            Assert.That(result[2].Point[elements[e]],
                Is.EqualTo((e == 2) ? 1.95 : 0).Within(0.1));
        }

        [Test]
        public static void Worldgen_Key_Has_Slot_For_Next_Key(
            [Range(0, 7)] int e)
        {
            //Arrange

            var GeneratorKey = new WorldgenPiece
            {
                PointSlot = ItemStore.Boots
                    .WithSignature(MagicGenerators.Signature.Value()).Done,
                Next = new WorldgenPiece()
                {
                    PointSlot = ItemStore.Gloves
                        .WithSignature(MagicGenerators.Signature.Value()).Done,
                    Next = new WorldgenPiece()
                    {
                        PointSlot =
                            ItemStore.Ring
                                .WithSignature(
                                    MagicGenerators.Signature.Value())
                                .Done,
                        Next = new WorldgenPiece
                        {
                            PointSlot = ItemStore.Boots
                                .WithElement(Element.Calor, 10).Done,
                            Next = new WorldgenPiece()
                            {
                                PointSlot = ItemStore.Gloves
                                    .WithElement(Element.Harmonius, -1).Done,
                                Next = new WorldgenPiece()
                                {
                                    PointSlot =
                                        ItemStore.Ring
                                            .WithElement(Element.Densitas, 2)
                                            .Done,
                                }
                            }
                        }
                    }
                }
            };
            //Assert
            var result = GeneratorKey.ResultingCurve();
            Assert.That(result[3].Point[elements[e]],
                Is.EqualTo((e == 1) ? 2.767 : 0).Within(0.001));
            Assert.That(result[4].Point[elements[e]],
                Is.EqualTo((e == 4) ? -1 : 0).Within(0.1));
            Assert.That(result[5].Point[elements[e]],
                Is.EqualTo((e == 2) ? 1.95 : 0).Within(0.1));
        }
    }
}
