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

public class RuntimeNetLogic2 : BaseNetLogic
{
    private IUAVariable varPLC;
    private RemoteVariableSynchronizer variableSynchronizer;

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        varPLC = Project.Current.GetVariable("CommDrivers/ModbusDriver1/ModbusStation1/Tags/ModbusTag1");
        variableSynchronizer = new RemoteVariableSynchronizer();
        variableSynchronizer.Add(varPLC);
        varPLC.VariableChange += VarPLC_VariableChange;
        CambiaColore(varPLC.Value);
    }

    private void VarPLC_VariableChange(object sender, VariableChangeEventArgs e)
    {
        CambiaColore(e.NewValue);
    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        varPLC.VariableChange -= VarPLC_VariableChange;
    }

    public void CambiaColore(int numeroColore)
    {
        var mioRettangolo = (Rectangle)Owner;
        if (numeroColore > 5)
            mioRettangolo.FillColor = Colors.Red;
        else
            mioRettangolo.FillColor = Colors.Yellow;

    }
}
