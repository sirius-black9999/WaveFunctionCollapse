using System;
using System.Numerics;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Voronoi;
using WaveFunctionTest.PropertyTesting.Tooling;

namespace WaveFunctionTest.VoronoiTest
{
    public class VoronoiGeneratorTest
    {
        [Test]
        public void Voronoi_Accepts_Points()
        {
            //Arrange
            var v = new Voronoi(LengthFuncs.EuclideanDistanceSqr,
                new Locus()
                {
                    position = new Vector2(1, 1),
                    effect = new Signature(new float[] { 0, 0, 0, 0, 0, 0, 0, 0 })
                },
                new Locus()
                {
                    position = new Vector2(2, 2),
                    effect = new Signature(new float[] { 1, 1, 1, 1, 1, 1, 1, 1 })
                });
            //Act
            var result = v.Points;
            //Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [DatapointSource] private InfiniLine[] vecs = Generator<InfiniLine>.Make(3, rand =>
        {
            var p1 = new Vector2(rand.NextFloat(-10, 10), rand.NextFloat(-10, 10));
            var p2 = new Vector2(rand.NextFloat(-10, 10), rand.NextFloat(-10, 10));
            return new InfiniLine(p1, p2);
        });

        [DatapointSource] private Vector2[] Positions = Generator<Vector2>.Make(5,
            rand => new Vector2(rand.NextFloat(-10, 10), rand.NextFloat(-10, 10)));

        [Theory]
        public void InfiniLine_Can_Trim_To_LineSegment(InfiniLine tested, InfiniLine border1, InfiniLine border2)
        {
            //Arrange
            Assume.That(tested.Parallel(border1), Is.False);
            Assume.That(tested.Parallel(border2), Is.False);
            Assume.That(Math.Sign(tested.IntersectionDistance(tested.Intersect(border1))),
                Is.Not.EqualTo(Math.Sign(tested.IntersectionDistance(tested.Intersect(border2)))));
            //Act
            var result = tested.TrimTo(border1, border2);
            //Assert

            Assert.That(new[] { result.p1, result.p2 },
                Is.EquivalentTo(new[] { tested.Intersect(border1), tested.Intersect(border2) }));
        }
    }
}
