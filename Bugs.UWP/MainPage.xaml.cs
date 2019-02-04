using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// ReSharper disable LoopCanBeConvertedToQuery


namespace Bugs.UWP {
	public sealed partial class MainPage {
		public MainPage() {
			InitializeComponent();
			var nonEmptyGroupInfos = new List<(int, int)> {
				(0, 2),
				(2, 1),
				(5, 3),
				(12, 1),
				(13, 1),
				(14, 2),
				(15, 4),
				(18, 1),
			};
			var collection = GenerateGroups(27, nonEmptyGroupInfos);
			ViewSource.Source = collection;
		}

		/// <summary>
        /// This simply generates the source for the CVS
        /// </summary>
        /// <param name="groupCount">The number of groups</param>
        /// <param name="nonEmptyGroupInfos">All groups whose index are not in this list will be empty.</param>
        /// <returns></returns>
		List<Group> GenerateGroups(int groupCount, IList<(int groupIdx, int childCount)> nonEmptyGroupInfos) {

			var groups = new List<Group>();
			foreach (var groupIdx in Enumerable.Range(0, groupCount)) {
				var nonEmptyGroup = nonEmptyGroupInfos.FirstOrDefault(x => x.groupIdx == groupIdx);
				var isEmpty = nonEmptyGroup == default(ValueTuple<int, int>);
				groups.Add(new Group($"group {groupIdx}", isEmpty ? 0 : nonEmptyGroup.childCount));
			}

            return groups;
		}
	}


	public class Group : List<string> {
		public Group(string header, int count) {
			Header = header;
			// NOTE: If all groups are not empty, then everything will work fine (all will be shown)
			var items = Enumerable.Range(0, count).Select(x => "an item");
			foreach (var str in items) {
				Add(str);
			}
		}

		public string Header { get; }
	}
}