//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5448
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// -------------------------------------------------------------
// 
//             Powered By： SR3.1(SmartRobot For SmartPersistenceLayer 3.1) 听棠
//             Created By： cwx99
//             Created Time： 2011/11/8 11:49:00
// 
// -------------------------------------------------------------
namespace BusinessEntity
{
    using System;
    using System.Collections;
    using System.Data;
    using PersistenceLayer;
    
    
    /// <summary>该类的摘要说明</summary>
    [Serializable()]
    public class Entity行情表 : EntityObject
    {
        
        /// <summary>ID</summary>
        public const string @__ID = "ID";
        
        /// <summary>证券代码</summary>
        public const string @__证券代码 = "证券代码";
        
        /// <summary>证券名称</summary>
        public const string @__证券名称 = "证券名称";
        
        /// <summary>昨收</summary>
        public const string @__昨收 = "昨收";
        
        /// <summary>今开</summary>
        public const string @__今开 = "今开";
        
        /// <summary>最高</summary>
        public const string @__最高 = "最高";
        
        /// <summary>最低</summary>
        public const string @__最低 = "最低";
        
        /// <summary>最新</summary>
        public const string @__最新 = "最新";
        
        /// <summary>成交量</summary>
        public const string @__成交量 = "成交量";
        
        /// <summary>成交额</summary>
        public const string @__成交额 = "成交额";
        
        /// <summary>买价1</summary>
        public const string @__买价1 = "买价1";
        
        /// <summary>买量1</summary>
        public const string @__买量1 = "买量1";
        
        /// <summary>买价2</summary>
        public const string @__买价2 = "买价2";
        
        /// <summary>买量2</summary>
        public const string @__买量2 = "买量2";
        
        /// <summary>买价3</summary>
        public const string @__买价3 = "买价3";
        
        /// <summary>买量3</summary>
        public const string @__买量3 = "买量3";
        
        /// <summary>买价4</summary>
        public const string @__买价4 = "买价4";
        
        /// <summary>买量4</summary>
        public const string @__买量4 = "买量4";
        
        /// <summary>买价5</summary>
        public const string @__买价5 = "买价5";
        
        /// <summary>买量5</summary>
        public const string @__买量5 = "买量5";
        
        /// <summary>卖价1</summary>
        public const string @__卖价1 = "卖价1";
        
        /// <summary>卖量1</summary>
        public const string @__卖量1 = "卖量1";
        
        /// <summary>卖价2</summary>
        public const string @__卖价2 = "卖价2";
        
        /// <summary>卖量2</summary>
        public const string @__卖量2 = "卖量2";
        
        /// <summary>卖价3</summary>
        public const string @__卖价3 = "卖价3";
        
        /// <summary>卖量3</summary>
        public const string @__卖量3 = "卖量3";
        
        /// <summary>卖价4</summary>
        public const string @__卖价4 = "卖价4";
        
        /// <summary>卖量4</summary>
        public const string @__卖量4 = "卖量4";
        
        /// <summary>卖价5</summary>
        public const string @__卖价5 = "卖价5";
        
        /// <summary>卖量5</summary>
        public const string @__卖量5 = "卖量5";
        
        /// <summary>成交时间</summary>
        public const string @__成交时间 = "成交时间";
        
        private int m_ID;
        
        private string m_证券代码;
        
        private string m_证券名称;
        
        private decimal m_昨收;
        
        private decimal m_今开;
        
        private decimal m_最高;
        
        private decimal m_最低;
        
        private decimal m_最新;
        
        private int m_成交量;
        
        private decimal m_成交额;
        
        private decimal m_买价1;
        
        private int m_买量1;
        
        private decimal m_买价2;
        
        private int m_买量2;
        
        private decimal m_买价3;
        
        private int m_买量3;
        
        private decimal m_买价4;
        
        private int m_买量4;
        
        private decimal m_买价5;
        
        private int m_买量5;
        
        private decimal m_卖价1;
        
        private int m_卖量1;
        
        private decimal m_卖价2;
        
        private int m_卖量2;
        
        private decimal m_卖价3;
        
        private int m_卖量3;
        
        private decimal m_卖价4;
        
        private int m_卖量4;
        
        private decimal m_卖价5;
        
        private int m_卖量5;
        
        private System.DateTime m_成交时间 = DateTime.MinValue;
        
        /// <summary>构造函数</summary>
        public Entity行情表()
        {
        }
        
        /// <summary>属性ID </summary>
        public int ID
        {
            get
            {
                return this.m_ID;
            }
            set
            {
                this.m_ID = value;
            }
        }
        
        /// <summary>属性证券代码 </summary>
        public string 证券代码
        {
            get
            {
                return this.m_证券代码;
            }
            set
            {
                this.m_证券代码 = value;
            }
        }
        
        /// <summary>属性证券名称 </summary>
        public string 证券名称
        {
            get
            {
                return this.m_证券名称;
            }
            set
            {
                this.m_证券名称 = value;
            }
        }
        
        /// <summary>属性昨收 </summary>
        public decimal 昨收
        {
            get
            {
                return this.m_昨收;
            }
            set
            {
                this.m_昨收 = value;
            }
        }
        
        /// <summary>属性今开 </summary>
        public decimal 今开
        {
            get
            {
                return this.m_今开;
            }
            set
            {
                this.m_今开 = value;
            }
        }
        
        /// <summary>属性最高 </summary>
        public decimal 最高
        {
            get
            {
                return this.m_最高;
            }
            set
            {
                this.m_最高 = value;
            }
        }
        
        /// <summary>属性最低 </summary>
        public decimal 最低
        {
            get
            {
                return this.m_最低;
            }
            set
            {
                this.m_最低 = value;
            }
        }
        
        /// <summary>属性最新 </summary>
        public decimal 最新
        {
            get
            {
                return this.m_最新;
            }
            set
            {
                this.m_最新 = value;
            }
        }
        
        /// <summary>属性成交量 </summary>
        public int 成交量
        {
            get
            {
                return this.m_成交量;
            }
            set
            {
                this.m_成交量 = value;
            }
        }
        
        /// <summary>属性成交额 </summary>
        public decimal 成交额
        {
            get
            {
                return this.m_成交额;
            }
            set
            {
                this.m_成交额 = value;
            }
        }
        
        /// <summary>属性买价1 </summary>
        public decimal 买价1
        {
            get
            {
                return this.m_买价1;
            }
            set
            {
                this.m_买价1 = value;
            }
        }
        
        /// <summary>属性买量1 </summary>
        public int 买量1
        {
            get
            {
                return this.m_买量1;
            }
            set
            {
                this.m_买量1 = value;
            }
        }
        
        /// <summary>属性买价2 </summary>
        public decimal 买价2
        {
            get
            {
                return this.m_买价2;
            }
            set
            {
                this.m_买价2 = value;
            }
        }
        
        /// <summary>属性买量2 </summary>
        public int 买量2
        {
            get
            {
                return this.m_买量2;
            }
            set
            {
                this.m_买量2 = value;
            }
        }
        
        /// <summary>属性买价3 </summary>
        public decimal 买价3
        {
            get
            {
                return this.m_买价3;
            }
            set
            {
                this.m_买价3 = value;
            }
        }
        
        /// <summary>属性买量3 </summary>
        public int 买量3
        {
            get
            {
                return this.m_买量3;
            }
            set
            {
                this.m_买量3 = value;
            }
        }
        
        /// <summary>属性买价4 </summary>
        public decimal 买价4
        {
            get
            {
                return this.m_买价4;
            }
            set
            {
                this.m_买价4 = value;
            }
        }
        
        /// <summary>属性买量4 </summary>
        public int 买量4
        {
            get
            {
                return this.m_买量4;
            }
            set
            {
                this.m_买量4 = value;
            }
        }
        
        /// <summary>属性买价5 </summary>
        public decimal 买价5
        {
            get
            {
                return this.m_买价5;
            }
            set
            {
                this.m_买价5 = value;
            }
        }
        
        /// <summary>属性买量5 </summary>
        public int 买量5
        {
            get
            {
                return this.m_买量5;
            }
            set
            {
                this.m_买量5 = value;
            }
        }
        
        /// <summary>属性卖价1 </summary>
        public decimal 卖价1
        {
            get
            {
                return this.m_卖价1;
            }
            set
            {
                this.m_卖价1 = value;
            }
        }
        
        /// <summary>属性卖量1 </summary>
        public int 卖量1
        {
            get
            {
                return this.m_卖量1;
            }
            set
            {
                this.m_卖量1 = value;
            }
        }
        
        /// <summary>属性卖价2 </summary>
        public decimal 卖价2
        {
            get
            {
                return this.m_卖价2;
            }
            set
            {
                this.m_卖价2 = value;
            }
        }
        
        /// <summary>属性卖量2 </summary>
        public int 卖量2
        {
            get
            {
                return this.m_卖量2;
            }
            set
            {
                this.m_卖量2 = value;
            }
        }
        
        /// <summary>属性卖价3 </summary>
        public decimal 卖价3
        {
            get
            {
                return this.m_卖价3;
            }
            set
            {
                this.m_卖价3 = value;
            }
        }
        
        /// <summary>属性卖量3 </summary>
        public int 卖量3
        {
            get
            {
                return this.m_卖量3;
            }
            set
            {
                this.m_卖量3 = value;
            }
        }
        
        /// <summary>属性卖价4 </summary>
        public decimal 卖价4
        {
            get
            {
                return this.m_卖价4;
            }
            set
            {
                this.m_卖价4 = value;
            }
        }
        
        /// <summary>属性卖量4 </summary>
        public int 卖量4
        {
            get
            {
                return this.m_卖量4;
            }
            set
            {
                this.m_卖量4 = value;
            }
        }
        
        /// <summary>属性卖价5 </summary>
        public decimal 卖价5
        {
            get
            {
                return this.m_卖价5;
            }
            set
            {
                this.m_卖价5 = value;
            }
        }
        
        /// <summary>属性卖量5 </summary>
        public int 卖量5
        {
            get
            {
                return this.m_卖量5;
            }
            set
            {
                this.m_卖量5 = value;
            }
        }
        
        /// <summary>属性成交时间 </summary>
        public System.DateTime 成交时间
        {
            get
            {
                return this.m_成交时间;
            }
            set
            {
                this.m_成交时间 = value;
            }
        }
    }
    
    /// Entity行情表执行类
    public abstract class Entity行情表Action
    {
        
        private Entity行情表Action()
        {
        }
        
        public static void Save(Entity行情表 obj)
        {
            if (obj!=null)
            {
                obj.Save();
            }
        }
        
        /// <summary>根据主键获取一个实体</summary>
        public static Entity行情表 RetrieveAEntity行情表(int ID)
        {
            Entity行情表 obj=new Entity行情表();
            obj.ID=ID;
            obj.Retrieve();
            if (obj.IsPersistent)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static EntityContainer RetrieveEntity行情表()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(Entity行情表));
            return rc.AsEntityContainer();
        }
        
        /// <summary>获取所有实体(EntityContainer)</summary>
        public static DataTable GetEntity行情表()
        {
            RetrieveCriteria rc=new RetrieveCriteria(typeof(Entity行情表));
            return rc.AsDataTable();
        }
    }
}
