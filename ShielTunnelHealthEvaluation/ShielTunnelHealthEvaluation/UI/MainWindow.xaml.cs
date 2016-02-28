﻿using ShieldTunnelHealthEvaluation.CORE.FuzzyAHP;
using ShieldTunnelHealthEvaluation.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShieldTunnelHealthEvaluation.TestGoWPF;
using Northwoods.GoXam.Model;

namespace ShieldTunnelHealthEvaluation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel viewModel;
        public MainWindow()
        {
             viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;
            InitializeComponent();
            HealthEvaluationGlobals.MyAHPIndexHierarchys = this.viewModel.MyAHPIndexHierarchys;//临时的全局化方案，后面再整体修改

            AHPIndexHierarchyUtil ahpHierarchyUtil = new AHPIndexHierarchyUtil(this.viewModel.MyAHPIndexHierarchys[0]);
            var model = new TreeModel<AHPIndexHierarchy, string>();
            model.ChildNodesPath = "ChildrenNames";
            model.NodeKeyPath = "Name";
            model.ParentNodePath = "ParentName";
            model.NodesSource = ahpHierarchyUtil.ahpIndexList;
            myDiagram.Model = model;
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnTestt_Click(object sender, RoutedEventArgs e)
        {
            TestWnd _testWnd = new TestWnd();
            _testWnd.Show();
        }

        private void btnResult_Click(object sender, RoutedEventArgs e)
        {
            DateTime evaluationDate = (DateTime)lbDates.SelectedItem;
            Calculation cal = new Calculation(viewModel.MyAHPIndexHierarchys[0], viewModel._monDataUtil.groupedDatas, evaluationDate);
        }

        private void btnCriterias_Click(object sender, RoutedEventArgs e)
        {
            CriteriaSettingWnd criteriaSettingWnd = new CriteriaSettingWnd();
            criteriaSettingWnd.Show();
        }

        private void btnTestTreeDiagram_Click(object sender, RoutedEventArgs e)
        {
            AHPIndexHierarchyUtil ahpHierarchyUtil = new AHPIndexHierarchyUtil(this.viewModel.MyAHPIndexHierarchys[0]);
            var model = new TreeModel<AHPIndexHierarchy, string>();
            model.ChildNodesPath = "ChildrenNames";
            model.NodeKeyPath = "Name";
            model.ParentNodePath = "ParentName";
            model.NodesSource = ahpHierarchyUtil.ahpIndexList;
            TestGoWPFWnd.model = model;
            TestGoWPFWnd testGoWpfWnd = new TestGoWPFWnd();
            testGoWpfWnd.Show();
        }

        private void btnCalculateHealthState_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddWeight_Click(object sender, RoutedEventArgs e)
        {
            //NewJudgementMatrixWnd weightWnd = new NewJudgementMatrixWnd(viewModel.MyAHPIndexHierarchys[0]);
            //weightWnd.Show();
        }

        private void btnLookUpWeight_Click(object sender, RoutedEventArgs e)
        {
            //LookUpJudgementMatrixWnd weightWnd = new LookUpJudgementMatrixWnd(viewModel.MyAHPIndexHierarchys[0]);
            //weightWnd.Show();
            JudgementMatrixExpertDateListWnd _judgementMatrixListWnd = new JudgementMatrixExpertDateListWnd();
            _judgementMatrixListWnd.Show();
        }
    }
}
