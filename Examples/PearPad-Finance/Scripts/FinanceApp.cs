#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PearPadRuntime;
using UnityEngine;
using UnityEngine.UIElements;

namespace PearPadFinanceRuntime
{
    internal static class FinanceApp
    {
        private static PearPadAppContext? context;
        private static VisualElement? root;
        private static string activeTab = "overview";
        private static string transactionSort = "day";
        private static bool transactionSortDescending = true;

        public static void Build(
            VisualElement target,
            PearPadAppContext appContext)
        {
            root = target;
            context = appContext;
            Render();
        }

        private static void Render()
        {
            if (root == null || context == null)
                return;

            root.Clear();

            PearFinanceSnapshotData snapshot =
                PearFinanceService.GetSnapshot();

            VisualElement shell = Column();
            shell.style.flexGrow = 1;
            root.Add(shell);

            VisualElement top = Row();
            top.style.height = 50;
            top.style.flexShrink = 0;
            top.style.alignItems = Align.Center;
            top.style.marginBottom = 10;
            shell.Add(top);

            Label heading = Label(
                activeTab == "overview" ? "Financial Overview" : "Transactions",
                20,
                FontStyle.Bold,
                context.Text);
            heading.style.flexGrow = 1;
            top.Add(heading);

            top.Add(TabButton(
                "OVERVIEW",
                "overview",
                activeTab == "overview"));
            top.Add(TabButton(
                "TRANSACTIONS",
                "transactions",
                activeTab == "transactions"));

            if (activeTab == "transactions")
                BuildTransactions(shell, snapshot);
            else
                BuildOverview(shell, snapshot);
        }

        private static void BuildOverview(
            VisualElement parent,
            PearFinanceSnapshotData snapshot)
        {
            VisualElement stats = Row();
            stats.style.height = 92;
            stats.style.flexShrink = 0;
            stats.style.marginBottom = 12;
            parent.Add(stats);

            stats.Add(StatCard(
                "TODAY INCOME",
                snapshot.todayIncome,
                new Color(0.40f, 0.86f, 0.31f, 1f)));
            stats.Add(StatCard(
                "TODAY EXPENSES",
                -snapshot.todayExpenses,
                new Color(0.94f, 0.34f, 0.34f, 1f)));
            stats.Add(StatCard(
                "TODAY NET",
                snapshot.todayNet,
                snapshot.todayNet >= 0
                    ? new Color(0.40f, 0.86f, 0.31f, 1f)
                    : new Color(0.94f, 0.34f, 0.34f, 1f)));
            stats.Add(StatCard(
                "WEEK NET",
                snapshot.weekNet,
                snapshot.weekNet >= 0
                    ? new Color(0.40f, 0.86f, 0.31f, 1f)
                    : new Color(0.94f, 0.34f, 0.34f, 1f)));

            VisualElement middle = Row();
            middle.style.flexGrow = 1;
            parent.Add(middle);

            VisualElement chartCard = Card();
            chartCard.style.flexGrow = 1;
            chartCard.style.marginRight = 12;
            chartCard.style.paddingLeft = 16;
            chartCard.style.paddingRight = 16;
            chartCard.style.paddingTop = 14;
            chartCard.style.paddingBottom = 14;
            middle.Add(chartCard);

            chartCard.Add(Label(
                "Last 7 Days",
                14,
                FontStyle.Bold,
                context!.Text));

            Label chartHint = Label(
                "Daily net result",
                9,
                FontStyle.Normal,
                context.Muted);
            chartHint.style.marginTop = 2;
            chartHint.style.marginBottom = 10;
            chartCard.Add(chartHint);

            BuildSevenDayChart(chartCard, snapshot);

            VisualElement side = Column();
            side.style.width = 260;
            side.style.flexShrink = 0;
            middle.Add(side);

            VisualElement wallet = Card();
            wallet.style.flexShrink = 0;
            wallet.style.paddingLeft = 15;
            wallet.style.paddingRight = 15;
            wallet.style.paddingTop = 14;
            wallet.style.paddingBottom = 14;
            side.Add(wallet);

            wallet.Add(Label(
                "CURRENT CASH",
                8.5f,
                FontStyle.Bold,
                context.Muted));

            Label cash = Label(
                string.IsNullOrWhiteSpace(snapshot.cash)
                    ? "—"
                    : snapshot.cash,
                22,
                FontStyle.Bold,
                new Color(0.40f, 0.86f, 0.31f, 1f));
            cash.style.marginTop = 3;
            wallet.Add(cash);

            VisualElement week = Card();
            week.style.flexGrow = 1;
            week.style.marginTop = 10;
            week.style.paddingLeft = 15;
            week.style.paddingRight = 15;
            week.style.paddingTop = 14;
            week.style.paddingBottom = 14;
            side.Add(week);

            week.Add(Label(
                "WEEK",
                8.5f,
                FontStyle.Bold,
                context.Muted));

            week.Add(MetricRow(
                "Income",
                snapshot.weekIncome,
                new Color(0.40f, 0.86f, 0.31f, 1f)));

            week.Add(MetricRow(
                "Expenses",
                -snapshot.weekExpenses,
                new Color(0.94f, 0.34f, 0.34f, 1f)));

            week.Add(MetricRow(
                "Previous week",
                snapshot.previousWeekNet,
                snapshot.previousWeekNet >= 0
                    ? new Color(0.40f, 0.86f, 0.31f, 1f)
                    : new Color(0.94f, 0.34f, 0.34f, 1f)));

            if (snapshot.loans != null &&
                snapshot.loans.Length > 0)
            {
                week.Add(Separator());
                week.Add(Label(
                    snapshot.loans.Length + " active loan" +
                    (snapshot.loans.Length == 1 ? "" : "s"),
                    9.5f,
                    FontStyle.Bold,
                    context.Muted));
            }
        }

        private static void BuildSevenDayChart(
            VisualElement parent,
            PearFinanceSnapshotData snapshot)
        {
            PearFinanceDayData[] days =
                snapshot.days ?? Array.Empty<PearFinanceDayData>();

            List<PearFinanceDayData> ordered =
                days.OrderBy(x => x.dayNumber).ToList();
            List<PearFinanceDayData> last =
                ordered.Skip(Math.Max(0, ordered.Count - 7)).ToList();

            if (last.Count == 0)
            {
                VisualElement empty = Card();
                empty.style.flexGrow = 1;
                empty.style.alignItems = Align.Center;
                empty.style.justifyContent = Justify.Center;
                empty.Add(Label(
                    "No financial history yet.",
                    12,
                    FontStyle.Bold,
                    context!.Muted));
                parent.Add(empty);
                return;
            }

            float maxAbs = Mathf.Max(
                1f,
                last.Max(day => Mathf.Abs(day.net)));

            VisualElement chart = Row();
            chart.style.flexGrow = 1;
            chart.style.alignItems = Align.FlexEnd;
            chart.style.justifyContent = Justify.SpaceBetween;
            chart.style.paddingTop = 8;
            parent.Add(chart);

            foreach (PearFinanceDayData day in last)
            {
                VisualElement column = Column();
                column.style.flexGrow = 1;
                column.style.alignItems = Align.Center;
                column.style.justifyContent = Justify.FlexEnd;
                column.style.marginLeft = 4;
                column.style.marginRight = 4;
                chart.Add(column);

                Label amount = Label(
                    CompactMoney(day.net),
                    8.5f,
                    FontStyle.Bold,
                    day.net >= 0
                        ? new Color(0.40f, 0.86f, 0.31f, 1f)
                        : new Color(0.94f, 0.34f, 0.34f, 1f));
                amount.style.marginBottom = 5;
                column.Add(amount);

                VisualElement track = new VisualElement();
                track.style.width = 34;
                track.style.height = 145;
                track.style.justifyContent = Justify.FlexEnd;
                track.style.backgroundColor =
                    new Color(1f, 1f, 1f, 0.035f);
                track.style.borderTopLeftRadius = 8;
                track.style.borderTopRightRadius = 8;
                track.style.borderBottomLeftRadius = 8;
                track.style.borderBottomRightRadius = 8;
                column.Add(track);

                VisualElement bar = new VisualElement();
                bar.style.width = Length.Percent(100);
                bar.style.height = Mathf.Clamp(
                    18f + 122f * Mathf.Abs(day.net) / maxAbs,
                    18f,
                    140f);
                bar.style.backgroundColor =
                    day.net >= 0
                        ? new Color(0.40f, 0.86f, 0.31f, 0.92f)
                        : new Color(0.94f, 0.34f, 0.34f, 0.92f);
                bar.style.borderTopLeftRadius = 7;
                bar.style.borderTopRightRadius = 7;
                bar.style.borderBottomLeftRadius = 7;
                bar.style.borderBottomRightRadius = 7;
                track.Add(bar);

                Label dayLabel = Label(
                    "Day " + day.dayNumber,
                    8.5f,
                    FontStyle.Normal,
                    context!.Muted);
                dayLabel.style.marginTop = 6;
                column.Add(dayLabel);
            }
        }

        private static void BuildTransactions(
            VisualElement parent,
            PearFinanceSnapshotData snapshot)
        {
            PearFinanceTransactionData[] transactions =
                snapshot.transactions ??
                Array.Empty<PearFinanceTransactionData>();

            List<PearFinanceTransactionData> sourceRows =
                transactions
                    .Where(item => item != null)
                    .ToList();

            VisualElement tableHead = Row();
            tableHead.style.height = 38;
            tableHead.style.minHeight = 38;
            tableHead.style.flexShrink = 0;
            tableHead.style.paddingLeft = 13;
            tableHead.style.paddingRight = 13;
            tableHead.style.alignItems = Align.Center;
            tableHead.style.backgroundColor = context!.Surface2;
            tableHead.style.position = Position.Relative;
            Round(tableHead, 9);
            parent.Add(tableHead);

            VisualElement transactionHeader = SortHeader(
                "TRANSACTION",
                "transaction",
                0,
                TextAnchor.MiddleLeft,
                () =>
                {
                    SetTransactionSort("transaction");
                    Render();
                });
            transactionHeader.style.flexGrow = 1;
            tableHead.Add(transactionHeader);

            VisualElement dayHeader = SortHeader(
                "DAY",
                "day",
                90,
                TextAnchor.MiddleCenter,
                () =>
                {
                    SetTransactionSort("day");
                    Render();
                });
            tableHead.Add(dayHeader);

            VisualElement amountHeader = SortHeader(
                "AMOUNT",
                "amount",
                130,
                TextAnchor.MiddleRight,
                () =>
                {
                    SetTransactionSort("amount");
                    Render();
                });
            tableHead.Add(amountHeader);

            // This viewport clips every transaction row below the fixed header.
            // Rows can no longer paint over the tabs/title/header.
            VisualElement scrollViewport = new VisualElement();
            scrollViewport.style.flexGrow = 1;
            scrollViewport.style.marginTop = 7;
            scrollViewport.style.overflow = Overflow.Hidden;
            scrollViewport.style.position = Position.Relative;
            parent.Add(scrollViewport);

            ScrollView scroll =
                new ScrollView(ScrollViewMode.Vertical);
            scroll.style.flexGrow = 1;
            scroll.style.position = Position.Absolute;
            scroll.style.left = 0;
            scroll.style.right = 0;
            scroll.style.top = 0;
            scroll.style.bottom = 0;
            scroll.verticalScrollerVisibility =
                ScrollerVisibility.Auto;
            scroll.horizontalScrollerVisibility =
                ScrollerVisibility.Hidden;
            scroll.contentContainer.style.flexDirection =
                FlexDirection.Column;
            scroll.contentContainer.style.flexGrow = 0;
            scroll.contentContainer.style.flexShrink = 0;
            scrollViewport.Add(scroll);

            // Faster wheel scrolling for long transaction histories.
            scroll.RegisterCallback<WheelEvent>(evt =>
            {
                float delta = evt.delta.y;
                if (Mathf.Abs(delta) < 0.01f)
                    return;

                scroll.verticalScroller.value = Mathf.Clamp(
                    scroll.verticalScroller.value + delta * 155f,
                    scroll.verticalScroller.lowValue,
                    scroll.verticalScroller.highValue);

                evt.StopPropagation();
            });

            List<PearFinanceTransactionData> rows =
                SortTransactions(sourceRows)
                    .Take(300)
                    .ToList();

            if (rows.Count == 0)
            {
                VisualElement empty = Card();
                empty.style.flexShrink = 0;
                empty.style.minHeight = 90;
                empty.style.alignItems = Align.Center;
                empty.style.justifyContent = Justify.Center;
                empty.Add(Label(
                    "No transactions available.",
                    12,
                    FontStyle.Bold,
                    context.Muted));
                scroll.Add(empty);
                return;
            }

            foreach (PearFinanceTransactionData item in rows)
            {
                VisualElement row = Row();
                row.style.minHeight = 48;
                row.style.flexShrink = 0;
                row.style.paddingLeft = 13;
                row.style.paddingRight = 13;
                row.style.marginBottom = 5;
                row.style.alignItems = Align.Center;
                row.style.backgroundColor = context.Surface;
                Border(row);
                Round(row, 9);

                VisualElement desc = Column();
                desc.style.flexGrow = 1;
                desc.style.justifyContent = Justify.Center;
                row.Add(desc);

                Label descTitle = Label(
                    string.IsNullOrWhiteSpace(item.description)
                        ? "Transaction"
                        : item.description,
                    10.5f,
                    FontStyle.Bold,
                    context.Text);
                descTitle.style.whiteSpace = WhiteSpace.NoWrap;
                desc.Add(descTitle);

                string detail = item.category ?? "";
                if (!string.IsNullOrWhiteSpace(item.time))
                {
                    detail = string.IsNullOrWhiteSpace(detail)
                        ? item.time
                        : detail + " · " + item.time;
                }

                if (!string.IsNullOrWhiteSpace(detail))
                {
                    desc.Add(Label(
                        detail,
                        8.2f,
                        FontStyle.Normal,
                        context.Muted));
                }

                string dayText =
                    !string.IsNullOrWhiteSpace(item.day)
                        ? item.day
                        : item.dayNumber > 0
                            ? "Day " + item.dayNumber
                            : "";

                Label day = Label(
                    dayText,
                    9.5f,
                    FontStyle.Normal,
                    context.Muted);
                day.style.width = 90;
                day.style.unityTextAlign =
                    TextAnchor.MiddleCenter;
                row.Add(day);

                Label amount = Label(
                    SignedMoney(item.amount),
                    11.5f,
                    FontStyle.Bold,
                    item.amount >= 0
                        ? new Color(0.40f, 0.86f, 0.31f, 1f)
                        : new Color(0.94f, 0.34f, 0.34f, 1f));
                amount.style.width = 130;
                amount.style.unityTextAlign =
                    TextAnchor.MiddleRight;
                amount.style.whiteSpace = WhiteSpace.NoWrap;
                row.Add(amount);

                scroll.Add(row);
            }
        }

        private static IEnumerable<PearFinanceTransactionData>
            SortTransactions(
                IEnumerable<PearFinanceTransactionData> rows)
        {
            List<PearFinanceTransactionData> source = rows.ToList();
            IEnumerable<(PearFinanceTransactionData item, int index)> indexed =
                source.Select((item, index) => (item, index));

            switch (transactionSort)
            {
                case "transaction":
                    return transactionSortDescending
                        ? indexed.OrderByDescending(
                                x => x.item.description ?? "",
                                StringComparer.OrdinalIgnoreCase)
                            .ThenByDescending(x => x.index)
                            .Select(x => x.item)
                        : indexed.OrderBy(
                                x => x.item.description ?? "",
                                StringComparer.OrdinalIgnoreCase)
                            .ThenBy(x => x.index)
                            .Select(x => x.item);

                case "amount":
                    // Descending = biggest incoming payment first.
                    // Ascending = biggest expense (most negative) first.
                    return transactionSortDescending
                        ? indexed.OrderByDescending(x => x.item.amount)
                            .ThenByDescending(x => x.index)
                            .Select(x => x.item)
                        : indexed.OrderBy(x => x.item.amount)
                            .ThenBy(x => x.index)
                            .Select(x => x.item);

                default:
                    // Default: newest transaction first.
                    return transactionSortDescending
                        ? indexed.OrderByDescending(x => x.item.dayNumber)
                            .ThenByDescending(x => x.item.timestamp)
                            .ThenByDescending(x => x.index)
                            .Select(x => x.item)
                        : indexed.OrderBy(x => x.item.dayNumber)
                            .ThenBy(x => x.item.timestamp)
                            .ThenBy(x => x.index)
                            .Select(x => x.item);
            }
        }

        private static void SetTransactionSort(string column)
        {
            if (string.Equals(
                transactionSort,
                column,
                StringComparison.OrdinalIgnoreCase))
            {
                transactionSortDescending =
                    !transactionSortDescending;
                return;
            }

            transactionSort = column;

            // Desired first-click behavior:
            // DAY -> newest first
            // TRANSACTION -> A-Z
            // AMOUNT -> biggest expense first
            switch (column)
            {
                case "transaction":
                    transactionSortDescending = false;
                    break;
                case "amount":
                    transactionSortDescending = false;
                    break;
                default:
                    transactionSortDescending = true;
                    break;
            }
        }

        private static VisualElement SortHeader(
            string title,
            string column,
            float width,
            TextAnchor alignment,
            Action click)
        {
            VisualElement header = new VisualElement();
            if (width > 0)
            {
                header.style.width = width;
                header.style.flexShrink = 0;
            }

            header.style.height = Length.Percent(100);
            header.style.flexDirection = FlexDirection.Row;
            header.style.alignItems = Align.Center;
            header.style.justifyContent =
                alignment == TextAnchor.MiddleRight
                    ? Justify.FlexEnd
                    : alignment == TextAnchor.MiddleCenter
                        ? Justify.Center
                        : Justify.FlexStart;
            header.style.paddingLeft = 3;
            header.style.paddingRight = 3;
            header.pickingMode = PickingMode.Position;

            string arrow = "";
            if (string.Equals(
                transactionSort,
                column,
                StringComparison.OrdinalIgnoreCase))
            {
                if (column == "transaction")
                {
                    arrow = transactionSortDescending
                        ? "  Z-A"
                        : "  A-Z";
                }
                else if (column == "amount")
                {
                    arrow = transactionSortDescending
                        ? "  ↑"
                        : "  ↓";
                }
                else
                {
                    arrow = transactionSortDescending
                        ? "  ↓"
                        : "  ↑";
                }
            }

            Label label = Label(
                title + arrow,
                8.5f,
                FontStyle.Bold,
                string.Equals(
                    transactionSort,
                    column,
                    StringComparison.OrdinalIgnoreCase)
                    ? context!.Accent
                    : context!.Muted);
            label.style.unityTextAlign = alignment;
            header.Add(label);

            header.AddManipulator(new Clickable(click));

            header.RegisterCallback<PointerEnterEvent>(_ =>
            {
                header.style.backgroundColor =
                    new Color(1f, 1f, 1f, 0.045f);
            });

            header.RegisterCallback<PointerLeaveEvent>(_ =>
            {
                header.style.backgroundColor = Color.clear;
            });

            return header;
        }

        private static VisualElement TabButton(
            string text,
            string tab,
            bool active)
        {
            VisualElement button = new VisualElement();
            button.style.width = 118;
            button.style.height = 34;
            button.style.marginLeft = 7;
            button.style.alignItems = Align.Center;
            button.style.justifyContent = Justify.Center;
            button.style.backgroundColor =
                active ? context!.Accent : context!.Surface2;
            Round(button, 9);
            Border(button);

            Label label = Label(
                text,
                8.5f,
                FontStyle.Bold,
                context.Text);
            label.style.unityTextAlign = TextAnchor.MiddleCenter;
            button.Add(label);

            button.AddManipulator(new Clickable(() =>
            {
                activeTab = tab;
                Render();
            }));

            return button;
        }

        private static VisualElement StatCard(
            string title,
            float amount,
            Color valueColor)
        {
            VisualElement card = Card();
            card.style.flexGrow = 1;
            card.style.height = 86;
            card.style.marginRight = 8;
            card.style.paddingLeft = 13;
            card.style.paddingRight = 13;
            card.style.paddingTop = 12;
            card.style.paddingBottom = 12;
            card.style.justifyContent = Justify.Center;

            card.Add(Label(
                title,
                8.2f,
                FontStyle.Bold,
                context!.Muted));

            Label value = Label(
                SignedMoney(amount),
                16,
                FontStyle.Bold,
                valueColor);
            value.style.marginTop = 3;
            value.style.whiteSpace = WhiteSpace.NoWrap;
            card.Add(value);

            return card;
        }

        private static VisualElement MetricRow(
            string title,
            float amount,
            Color color)
        {
            VisualElement row = Row();
            row.style.minHeight = 36;
            row.style.flexShrink = 0;
            row.style.alignItems = Align.Center;

            Label left = Label(
                title,
                9.5f,
                FontStyle.Normal,
                context!.Muted);
            left.style.flexGrow = 1;
            row.Add(left);

            row.Add(Label(
                SignedMoney(amount),
                10.5f,
                FontStyle.Bold,
                color));

            return row;
        }

        private static VisualElement Card()
        {
            VisualElement element = new VisualElement();
            element.style.backgroundColor = context!.Surface;
            Border(element);
            Round(element, 13);
            return element;
        }

        private static VisualElement Row()
        {
            VisualElement e = new VisualElement();
            e.style.flexDirection = FlexDirection.Row;
            return e;
        }

        private static VisualElement Column()
        {
            VisualElement e = new VisualElement();
            e.style.flexDirection = FlexDirection.Column;
            return e;
        }

        private static Label Label(
            string text,
            float size,
            FontStyle style,
            Color color)
        {
            Label label = new Label(text ?? "");
            label.style.fontSize = size;
            label.style.unityFontStyleAndWeight = style;
            label.style.color = color;
            return label;
        }

        private static VisualElement Separator()
        {
            VisualElement line = new VisualElement();
            line.style.height = 1;
            line.style.flexShrink = 0;
            line.style.marginTop = 8;
            line.style.marginBottom = 8;
            line.style.backgroundColor =
                new Color(1f, 1f, 1f, 0.08f);
            return line;
        }

        private static void Border(VisualElement e)
        {
            Color c = new Color(1f, 1f, 1f, 0.09f);
            e.style.borderLeftWidth = 1;
            e.style.borderRightWidth = 1;
            e.style.borderTopWidth = 1;
            e.style.borderBottomWidth = 1;
            e.style.borderLeftColor = c;
            e.style.borderRightColor = c;
            e.style.borderTopColor = c;
            e.style.borderBottomColor = c;
        }

        private static void Round(
            VisualElement e,
            float radius)
        {
            e.style.borderTopLeftRadius = radius;
            e.style.borderTopRightRadius = radius;
            e.style.borderBottomLeftRadius = radius;
            e.style.borderBottomRightRadius = radius;
        }

        private static string SignedMoney(float value)
        {
            string sign = value > 0.005f
                ? "+"
                : value < -0.005f
                    ? "-"
                    : "";

            return sign + "$" +
                Mathf.Abs(value).ToString(
                    "N0",
                    CultureInfo.InvariantCulture);
        }

        private static string CompactMoney(float value)
        {
            float abs = Mathf.Abs(value);
            string sign = value < 0 ? "-" : value > 0 ? "+" : "";

            if (abs >= 1000000000f)
                return sign + "$" +
                    (abs / 1000000000f).ToString("0.#") + "B";
            if (abs >= 1000000f)
                return sign + "$" +
                    (abs / 1000000f).ToString("0.#") + "M";
            if (abs >= 1000f)
                return sign + "$" +
                    (abs / 1000f).ToString("0.#") + "K";

            return sign + "$" + abs.ToString("0");
        }
    }
}
