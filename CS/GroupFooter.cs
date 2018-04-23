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

namespace Q484501GroupFooterWorkaround {
    public class GroupFooter : Control {
        public static readonly DependencyProperty GroupRowDataProperty =
            DependencyProperty.Register("GroupRowData", typeof(MyGroupRowData), typeof(GroupFooter), new PropertyMetadata(null));
        public static readonly DependencyProperty RowHandleProperty =
            DependencyProperty.Register("RowHandle", typeof(RowHandle), typeof(GroupFooter), new PropertyMetadata(new RowHandle(GridControl.InvalidRowHandle), (d, e) => ((GroupFooter)d).RefreshContent()));

        public GroupFooter() {
            DefaultStyleKey = typeof(GroupFooter);
        }

        public MyGroupRowData GroupRowData {
            get { return (MyGroupRowData)GetValue(GroupRowDataProperty); }
            set { SetValue(GroupRowDataProperty, value); }
        }
        public RowHandle RowHandle {
            get { return (RowHandle)GetValue(RowHandleProperty); }
            set { SetValue(RowHandleProperty, value); }
        }

        public MyTableView View { get { return DataContext == null ? null : (MyTableView)((RowData)DataContext).View; } }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            RefreshContent();
        }
        public bool IsLastRowInGroup(int groupRowHandle) {
            GridControl gridControl = View.DataControl as GridControl;
            if(!gridControl.IsValidRowHandle(groupRowHandle))
                return false;
            int childrenCount = gridControl.GetChildRowCount(groupRowHandle);
            int lastRowHandle = gridControl.GetChildRowHandle(groupRowHandle, childrenCount - 1);
            return gridControl.GetRowVisibleIndexByHandle(lastRowHandle) <= gridControl.GetRowVisibleIndexByHandle(RowHandle.Value);
        }
        public void RefreshContent() {
            if(View == null)
                return;
            RowHandle groupRowHandle = GetParentRowHandle();
            View.AddToCacheGroupFooter(this);
            UpdateVisibility(groupRowHandle);
            if(Visibility == Visibility.Collapsed)
                return;
            UpdateGroupFooterSummaryContent(groupRowHandle);
            UpdateOffset();
        }
        void UpdateOffset() {
            double offset = View.LeftGroupAreaIndent * ((GridControl)View.DataControl).GroupCount;
            Margin = new Thickness(-offset, 0, 0, 0);
        }
        void UpdateGroupFooterSummaryContent(RowHandle groupRowHandle) {
            if(GroupRowData == null)
                GroupRowData = View.CreateGroupRowData(groupRowHandle);
            else
                View.UpdateGroupRowData(GroupRowData, groupRowHandle);
        }
        RowHandle GetParentRowHandle() {
            return new RowHandle(((GridControl)View.DataControl).GetParentRowHandle(RowHandle.Value));
        }
        void UpdateVisibility(RowHandle groupRowHandle) {
            Visibility = IsLastRowInGroup(groupRowHandle.Value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }    
}
