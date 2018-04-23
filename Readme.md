# DXGrid - How to show group summaries in a group footer


<p>This example demonstrates how to create group footers in the grid and show summaries there. For this, you will need to create a TableView descendant and use custom templates for ordinary and group rows. Please note that the demonstrated approach has certain limitations:</p><p>1) It works only for one-level grouping; </p><p>2) The row indicator for a row that contains a group footer will be drawn both for the row and for the footer; </p><p>3) The group footer will replicate summaries that can be shown in the group row; </p><p>4) It is necessary to create a group footer template for each theme used in the application.</p>

<br/>


