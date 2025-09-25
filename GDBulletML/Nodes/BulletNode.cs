
namespace GDBulletML
{
	public partial class BulletNode : BulletMLNode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GDBulletML.BulletNode"/> class.
		/// </summary>
		public BulletNode(IBulletManager manager) : this(ENodeName.bullet, manager)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="GDBulletML.BulletNode"/> class.
		/// this is the constructor used by sub classes
		/// </summary>
		/// <param name="eNodeType">the node type.</param>
		public BulletNode(ENodeName nodeType, IBulletManager manager) : base(nodeType, manager)
		{
		}
	}
}
