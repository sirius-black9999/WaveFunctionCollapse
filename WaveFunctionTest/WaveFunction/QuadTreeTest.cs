using NUnit.Framework;
using WaveFunction;
using WaveFunction.WaveFunc;

namespace WaveFunctionTest.WaveFunction;

public class QuadTreeTest
{
    [Test]
    public void A_QuadTree_Contains_A_Root_Node()
    {
        //Arrange
        var tree = new QuadTree();
        //Act
        var ret = tree.Root;
        //Assert
        Assert.That(ret, Is.InstanceOf<QuadNode>());
    }

    [Test]
    public void A_QuadNode_Contains_Four_Nullable_Children()
    {
        //Arrange
        var tree = new QuadNode();
        //Act
        var tl = tree[Corner.TopLeft];
        var tr = tree[Corner.TopRight];
        var bl = tree[Corner.BotLeft];
        var br = tree[Corner.BotRight];
        //Assert
        Assert.That(tl, Is.Null);
        Assert.That(tr, Is.Null);
        Assert.That(bl, Is.Null);
        Assert.That(br, Is.Null);
    }

    [Test]
    public void A_QuadNode_Corners_May_Be_Set()
    {
        //Arrange
        var tree = new QuadNode
        {
            [Corner.TopLeft] = new QuadNode(),
            [Corner.TopRight] = new QuadNode(),
            [Corner.BotLeft] = new QuadNode(),
            [Corner.BotRight] = new QuadNode()
        };
        //Act
        var tl = tree[Corner.TopLeft];
        var tr = tree[Corner.TopRight];
        var bl = tree[Corner.BotLeft];
        var br = tree[Corner.BotRight];
        //Assert
        Assert.That(tl, Is.InstanceOf<QuadNode>());
        Assert.That(tr, Is.InstanceOf<QuadNode>());
        Assert.That(bl, Is.InstanceOf<QuadNode>());
        Assert.That(br, Is.InstanceOf<QuadNode>());
    }

    [Test]
    public void Quick_Init_Function()
    {
        //Arrange
        var tree = QuadNode.MakeFull();
        //Act
        var tl = tree[Corner.TopLeft];
        var tr = tree[Corner.TopRight];
        var bl = tree[Corner.BotLeft];
        var br = tree[Corner.BotRight];
        //Assert
        Assert.That(tl, Is.InstanceOf<QuadNode>());
        Assert.That(tr, Is.InstanceOf<QuadNode>());
        Assert.That(bl, Is.InstanceOf<QuadNode>());
        Assert.That(br, Is.InstanceOf<QuadNode>());
    }

    [Test]
    public void Quick_Fill_Function()
    {
        //Arrange
        var tree = new QuadNode();
        //Act
        var t2 = tree.Fill();
        //Assert
        var tl = tree[Corner.TopLeft];
        var tr = tree[Corner.TopRight];
        var bl = tree[Corner.BotLeft];
        var br = tree[Corner.BotRight];
        Assert.That(tl, Is.InstanceOf<QuadNode>());
        Assert.That(tr, Is.InstanceOf<QuadNode>());
        Assert.That(bl, Is.InstanceOf<QuadNode>());
        Assert.That(br, Is.InstanceOf<QuadNode>());
        Assert.That(t2, Is.EqualTo(tree));
    }

    [Test]
    public void Recurrent_Init_Function()
    {
        //Arrange
        var tree = QuadNode.MakeFull(static () => QuadNode.MakeFull());
        //Act
        var tl = tree[Corner.TopLeft][Corner.TopRight];
        var tr = tree[Corner.TopRight][Corner.BotLeft];
        var bl = tree[Corner.BotLeft][Corner.TopLeft];
        var br = tree[Corner.BotRight][Corner.BotRight];
        //Assert
        Assert.That(tl, Is.InstanceOf<QuadNode>());
        Assert.That(tr, Is.InstanceOf<QuadNode>());
        Assert.That(bl, Is.InstanceOf<QuadNode>());
        Assert.That(br, Is.InstanceOf<QuadNode>());
    }

    [Test]
    public void Recurrent_Init_Function_Only_Acts_Once()
    {
        //Arrange
        var tree = QuadNode.MakeFull(static () => new QuadNode().Fill(() => new QuadNode()));
        //Act
        var tl = tree[Corner.TopLeft][Corner.TopRight][Corner.TopLeft];
        var tr = tree[Corner.TopRight][Corner.BotLeft][Corner.TopLeft];
        var bl = tree[Corner.BotLeft][Corner.TopLeft][Corner.TopLeft];
        var br = tree[Corner.BotRight][Corner.BotRight][Corner.TopLeft];
        //Assert
        Assert.That(tl, Is.Null);
        Assert.That(tr, Is.Null);
        Assert.That(bl, Is.Null);
        Assert.That(br, Is.Null);
    }
}
