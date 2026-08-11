PearPad: Finance 0.1.4

TEST ADD-ON FOR PEARPAD CORE 1.02

This is a separate Big Ambitions mod.
It requires PearPad Core 1.02.

Purpose:
- prove that an external PearPad app can register itself
- appear automatically in the PearPad launcher
- appear automatically in the PearPad launcher without PearPad Core knowing about the app at compile time
- render its page inside the PearPad workspace

Finance features:
- Today Income
- Today Expenses
- Today Net
- Week Net
- Current Cash
- Week Income / Expenses
- Previous Week result
- 7-day net bar chart
- Recent transaction table
- detected loan count
- Overview / Transactions tabs

Expected log:
[PearPad: Finance] Registered with PearPad Core.

Expected launcher:
A green Finance icon appears automatically.


WARNING CLEANUP
- Finance icon loader now has a nullable return contract.
- Removed compile-time unreachable API version branch.
- No behavior change to Finance registration.


0.1.1 TRANSACTION POLISH
- Transactions are explicitly sorted newest first by day, timestamp and time.
- Mouse-wheel scrolling is much faster.
- Live transaction search added.
- Search filters description, category, day, time, source and amount as you type.
- Search does not rebuild the Finance app page.
- Up to 300 matching rows are shown.


0.1.2 TRANSACTION TABLE
- Removed the live search field.
- Transaction header, Day header and Amount header are clickable sorting controls.
- Default sort: newest transactions first.
- Day toggles newest/oldest.
- Transaction toggles A-Z/Z-A.
- Amount first click shows biggest expenses first; second click shows biggest incoming payments first.
- Active sort is marked in the header.
- Transaction rows are clipped inside their own viewport so they cannot scroll over fixed Finance UI.
- Faster mouse-wheel scrolling retained.


0.1.3 UNITY COMPATIBILITY
- Removed unsupported IStyle.zIndex usage.
- Fixed-header behavior still uses a clipped transaction viewport.
- Sorting and fast scrolling are unchanged.


0.1.4
- Fixed default transaction order so newest entries appear at the top.
- Applied the same ordering fix to the wallet transaction list.
