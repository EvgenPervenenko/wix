// Copyright (c) .NET Foundation and contributors. All rights reserved. Licensed under the Microsoft Reciprocal License. See LICENSE.TXT file in the project root for full license information.

namespace WixToolsetTest.MsiE2E;

using System;
using System.IO;
using WixTestTools;
using Xunit;
using Xunit.Abstractions;

public class RemoveFolderExTests : MsiE2ETests
{
    public RemoveFolderExTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void CanValidateRemoveFolderExFile()
    {
        var removeFolderExTestDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RemoveFolderExTest");
        var removeFolderExTestPath = Path.Combine(removeFolderExTestDir, "249_symbols_file_name_249_symbols_file_name_249_symbols_file_name_249_symbols_file_name_249_symbols_file_name_249_249_symbols_file_name_249_symbols_file_name_249_symbols_file_name_249_symbols_file_name_249_symbols_file_name_249_symbols_file_name.txt");

        try
        {
            Directory.CreateDirectory(removeFolderExTestDir);
            File.Create(removeFolderExTestPath).Dispose();

            Assert.True(File.Exists(removeFolderExTestPath), $"Create {removeFolderExTestPath} failed.");

            var product = this.CreatePackageInstaller("RemoveFolderExTest");
            product.InstallProduct(MSIExec.MSIExecReturnCode.SUCCESS);
            
            Assert.False(File.Exists(removeFolderExTestPath), $"Remove {removeFolderExTestDir} failed.");

            Directory.CreateDirectory(removeFolderExTestDir);
            File.Create(removeFolderExTestPath).Dispose();

            Assert.True(File.Exists(removeFolderExTestPath), $"Create {removeFolderExTestPath} failed.");

            product.UninstallProduct(MSIExec.MSIExecReturnCode.SUCCESS);

            Assert.False(File.Exists(removeFolderExTestPath), $"Remove {removeFolderExTestDir} failed.");
        }
        finally
        {
            if (Directory.Exists(removeFolderExTestDir))
            {
                Directory.Delete(removeFolderExTestDir, true);
            }
        }
    }
}
