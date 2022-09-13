using NUnit.Framework;
using WaveFunction;

namespace WaveFunctionTest;

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
        var TL = tree[Corner.TopLeft];
        var TR = tree[Corner.TopRight];
        var BL = tree[Corner.BotLeft];
        var BR = tree[Corner.BotRight];
        //Assert
        Assert.That(TL, Is.Null);
        Assert.That(TR, Is.Null);
        Assert.That(BL, Is.Null);
        Assert.That(BR, Is.Null);
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
        var TL = tree[Corner.TopLeft];
        var TR = tree[Corner.TopRight];
        var BL = tree[Corner.BotLeft];
        var BR = tree[Corner.BotRight];
        //Assert
        Assert.That(TL, Is.InstanceOf<QuadNode>());
        Assert.That(TR, Is.InstanceOf<QuadNode>());
        Assert.That(BL, Is.InstanceOf<QuadNode>());
        Assert.That(BR, Is.InstanceOf<QuadNode>());
    }

    [Test]
    public void Quick_Init_Function()
    {
        //Arrange
        var tree = QuadNode.MakeFull();
        //Act
        var TL = tree[Corner.TopLeft];
        var TR = tree[Corner.TopRight];
        var BL = tree[Corner.BotLeft];
        var BR = tree[Corner.BotRight];
        //Assert
        Assert.That(TL, Is.InstanceOf<QuadNode>());
        Assert.That(TR, Is.InstanceOf<QuadNode>());
        Assert.That(BL, Is.InstanceOf<QuadNode>());
        Assert.That(BR, Is.InstanceOf<QuadNode>());
    }

    [Test]
    public void Quick_Fill_Function()
    {
        //Arrange
        var tree = new QuadNode();
        //Act
        var t2 = tree.Fill();
        //Assert
        var TL = tree[Corner.TopLeft];
        var TR = tree[Corner.TopRight];
        var BL = tree[Corner.BotLeft];
        var BR = tree[Corner.BotRight];
        Assert.That(TL, Is.InstanceOf<QuadNode>());
        Assert.That(TR, Is.InstanceOf<QuadNode>());
        Assert.That(BL, Is.InstanceOf<QuadNode>());
        Assert.That(BR, Is.InstanceOf<QuadNode>());
        Assert.That(t2, Is.EqualTo(tree));
    }

    [Test]
    public void Recurrent_Init_Function()
    {
        //Arrange
        var tree = QuadNode.MakeFull(static () => QuadNode.MakeFull());
        //Act
        var TL = tree[Corner.TopLeft][Corner.TopRight];
        var TR = tree[Corner.TopRight][Corner.BotLeft];
        var BL = tree[Corner.BotLeft][Corner.TopLeft];
        var BR = tree[Corner.BotRight][Corner.BotRight];
        //Assert
        Assert.That(TL, Is.InstanceOf<QuadNode>());
        Assert.That(TR, Is.InstanceOf<QuadNode>());
        Assert.That(BL, Is.InstanceOf<QuadNode>());
        Assert.That(BR, Is.InstanceOf<QuadNode>());
    }

    [Test]
    public void Recurrent_Init_Function_Only_Acts_Once()
    {
        //Arrange
        var tree = QuadNode.MakeFull(static () => new QuadNode().Fill(() => new QuadNode()));
        //Act
        var TL = tree[Corner.TopLeft][Corner.TopRight][Corner.TopLeft];
        var TR = tree[Corner.TopRight][Corner.BotLeft][Corner.TopLeft];
        var BL = tree[Corner.BotLeft][Corner.TopLeft][Corner.TopLeft];
        var BR = tree[Corner.BotRight][Corner.BotRight][Corner.TopLeft];
        //Assert
        Assert.That(TL, Is.Null);
        Assert.That(TR, Is.Null);
        Assert.That(BL, Is.Null);
        Assert.That(BR, Is.Null);
    }
}
