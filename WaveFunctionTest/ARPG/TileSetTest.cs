using System;
using NUnit.Framework;
using WaveFunction.ARPG.Battle;
using WaveFunction.ARPG.Tiles;
using WaveFunction.MagicSystemSketch;

namespace WaveFunctionTest.ARPG
{
    public class TileSetTest
    {
        [DatapointSource] private NavDir[] Dirs => Enum.GetValues<NavDir>();

        [Theory]
        public void A_Tile_Has_A_Signature_Along_Each_Edge(NavDir d)
        {
            //Arrange
            var tile = new SetTile();
            //Act
            //Assert
            Assert.That(tile.SideSignature(d), Is.EqualTo(new Signature(0, 0, 0, 0, 0, 0, 0, 0)));
        }
    }
}
