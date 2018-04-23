using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Native;

namespace Q484501GroupFooterWorkaround {
    public class MyTableView : TableView {
        public List<GroupFooter> GroupFooterCollection { get; private set; }
        public MyTableView() {
            GroupFooterCollection = new List<GroupFooter>();
        }
        public void AddToCacheGroupFooter(GroupFooter groupFooter) {
            if(!GroupFooterCollection.Contains(groupFooter))
                GroupFooterCollection.Add(groupFooter);
        }
        public MyGroupRowData CreateGroupRowData(RowHandle groupRowHandle) {
            MyGroupRowData rowData = new MyGroupRowData(visualDataTreeBuilder);
            UpdateGroupRowData(rowData, groupRowHandle);
            UpdateFixedNoneContentWidth(rowData);
            return rowData;
        }
        public void UpdateGroupRowData(MyGroupRowData rowData, RowHandle rowHandle) {
            rowData.UpdateRowHandle(rowHandle);
            UpdateCellData(rowData);
            rowData.UpdateMyGroupSummaryData();            
        }
        protected override void OnUpdateRowsCore() {
            base.OnUpdateRowsCore();
            ForeachMyGroupRowData(e => e.UpdateRowData(), f => f.RefreshContent());
        }
        protected override void UpdateGroupSummary() {
            base.UpdateGroupSummary();
            ForeachMyGroupRowData(e => e.UpdateMyGroupSummaryData());
        }
        protected override void UpdateCellData() {
            base.UpdateCellData();
            ForeachMyGroupRowData(e => UpdateCellData(e));
        }
        protected override void UpdateRowData(UpdateRowDataDelegate updateMethod, bool updateInvisibleRows = true, bool updateFocusedRow = true) {
            base.UpdateRowData(updateMethod, updateInvisibleRows, updateFocusedRow);
            ForeachMyGroupRowData(e => updateMethod(e));
        }

        void ForeachMyGroupRowData(Action<MyGroupRowData> groupDataAction, Action<GroupFooter> groupFooterAction = null) {
            foreach(GroupFooter groupFooter in GroupFooterCollection) {
                if(groupFooterAction != null) groupFooterAction(groupFooter);
                if(groupFooter.GroupRowData != null)
                    groupDataAction(groupFooter.GroupRowData);
            }
        }
    }

    public class MyGroupRowData : GroupRowData {
        public MyGroupRowData(DataTreeBuilder treeBuilder)
            : base(treeBuilder) { }

        public void UpdateRowData() {
            UpdateData();
        }
        public void UpdateRowHandle(RowHandle rowHandle) {
            SetRowHandle(rowHandle);
        }
        public void UpdateMyGroupSummaryData() {
            UpdateGroupSummaryData();
        }
    }
}