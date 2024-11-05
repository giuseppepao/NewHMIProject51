#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.UI;
using FTOptix.DataLogger;
using FTOptix.NativeUI;
using FTOptix.Recipe;
using FTOptix.SQLiteStore;
using FTOptix.Store;
using FTOptix.ODBCStore;
using FTOptix.Modbus;
using FTOptix.Retentivity;
using FTOptix.CoreBase;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.OPCUAServer;
using FTOptix.Alarm;
#endregion

public class DesignTimeNetLogic1 : BaseNetLogic
{
    [ExportMethod]
    public void Test()
    {
        // Insert code to be executed by the method
        Log.Info("Test mio script designtime");
    }

    [ExportMethod]
    public void CreaAllarmi()
    {
        var numeroAllarmi = LogicObject.GetVariable("NumeroAllarmi");
        var PLCVariable = Project.Current.GetVariable("Model/AllarmiPLC");

        for (int i = 0; i < numeroAllarmi.Value; i++)
        {
            var mioAllarme = InformationModel.Make<DigitalAlarm>("Allarme" + i);
            mioAllarme.Message = "Mio allarme " + i;
            mioAllarme.InputValueVariable.SetDynamicLink(PLCVariable, (uint)i, DynamicLinkMode.ReadWrite);
            Project.Current.Get("Alarms").Add(mioAllarme);
        }
    }
}
