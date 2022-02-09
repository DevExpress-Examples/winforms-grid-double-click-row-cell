<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128629003/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E595)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->

# Data Grid for Windows Forms - How to handle a double-click on a grid row or cell

This example shows how to respond to double-click mouse events within rows, cells, and cell editors in a [Grid Control](https://docs.devexpress.com/WindowsForms/3455/controls-and-libraries/data-grid). The code demonstrates how to implement this task when cell edit operations are disabled and enabled.

The grid does not fire its own [DoubleClick](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Base.BaseView.DoubleClick) event when a user double-clicks within an active cell editor. This example shows how to intercept mouse double-click actions within cell editors.

<!-- default file list -->
## Files to Look At
* [Form1.cs](./CS/DoubleClickCell/Form1.cs) (VB: [Form1.vb](./VB/DoubleClickCell/Form1.vb))
<!-- default file list end -->

## Documentation
- [How to: Handle a Double-Click on a Grid Row or Cell](https://docs.devexpress.com/WindowsForms/403568/controls-and-libraries/data-grid/examples/navigation-and-selection/how-to-handle-a-double-click-on-a-grid-row-or-cell)
- [BaseView.DoubleClick](https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Base.BaseView.DoubleClick)
