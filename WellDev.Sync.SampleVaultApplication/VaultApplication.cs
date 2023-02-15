using MFiles.VAF;
using MFiles.VAF.AppTasks;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFilesAPI;
using System;
using System.Diagnostics;
using System.IO;

namespace WellDev.Sync.SampleVaultApplication
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication
        : ConfigurableVaultApplicationBase<Configuration>
    {

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterFileUpload)]
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize)]
        public void AfterFileUploadEvent(EventHandlerEnvironment env)
        {
            var data = env.ObjVer.Serialize();
           
        }
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize)]
        public void AfterCreateNewObjectFinalizeEvent(EventHandlerEnvironment env)
        {
            var data = env.ObjVer.Serialize();

        }

        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCheckInChanges)]
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterFileUpload)]
        protected void HandleAfterCheckInChangesEvent(EventHandlerEnvironment env)
        {
            var currentVault = env.Vault;
            var byteData = env.ObjVer.Serialize();
            var currentObjectId = new MFilesAPI.ObjID();
            currentObjectId.SetIDs(ObjType: (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument, ID: env.ObjVer.ID);
            var objectVersion = currentVault.ObjectOperations.GetObjectInfo(env.ObjVer, true);
            var name  =  objectVersion.GetNameForFileSystem();
            var nameEx = objectVersion.GetNameForFileSystemEx();
            var stream = File.Create("C:\\"+name);
            stream.Write(byteData, 0, byteData.Length);
            stream.Close();

            // helper class 

            //MFFileHelper.
        }
    }
}