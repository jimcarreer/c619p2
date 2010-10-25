using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hades
{
    class RedBlackTree : BinaryTree
    {
        #region Protected Methods
        protected override Node Delete(Node subroot, Person p)
        {
            return base.Delete(subroot, p);
        }

        protected override Node Insert(Node subroot, Person p)
        {
            return base.Insert(subroot, p);
        }

        protected override Node Search(Node subroot, Person p, ref int depth)
        {
            return base.Search(subroot, p, ref depth);
        }
        #endregion
    }
}
