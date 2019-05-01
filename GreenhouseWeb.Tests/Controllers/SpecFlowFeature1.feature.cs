We could not find a data exchange file at the path System.Configuration.ConfigurationErrorsException: SpecFlow configuration error ---> System.Configuration.ConfigurationErrorsException: The element <unitTestProvider> may only appear once in this section.

Please open an issue at https://github.com/techtalk/SpecFlow/issues/
Complete output: 
System.Configuration.ConfigurationErrorsException: SpecFlow configuration error ---> System.Configuration.ConfigurationErrorsException: The element <unitTestProvider> may only appear once in this section.
   at System.Configuration.ConfigurationElement.DeserializeElement(XmlReader reader, Boolean serializeCollectionKey)
   at TechTalk.SpecFlow.Configuration.ConfigurationSectionHandler.CreateFromXml(String xmlContent)
   at TechTalk.SpecFlow.Generator.Configuration.GeneratorConfigurationProvider.GetPlugins(SpecFlowConfigurationHolder configurationHolder)
   --- End of inner exception stack trace ---
   at TechTalk.SpecFlow.Generator.Configuration.GeneratorConfigurationProvider.GetPlugins(SpecFlowConfigurationHolder configurationHolder)
   at TechTalk.SpecFlow.Generator.GeneratorContainerBuilder.LoadPlugins(ObjectContainer container, IGeneratorConfigurationProvider configurationProvider, SpecFlowConfigurationHolder configurationHolder)
   at TechTalk.SpecFlow.Generator.GeneratorContainerBuilder.CreateContainer(SpecFlowConfigurationHolder configurationHolder, ProjectSettings projectSettings)
   at TechTalk.SpecFlow.Generator.TestGeneratorFactory.CreateGenerator(ProjectSettings projectSettings)
   at TechTalk.SpecFlow.VisualStudio.CodeBehindGenerator.Actions.GenerateTestFileAction.GenerateTestFile(GenerateTestFileParameters opts)



Command: C:\USERS\HCHB\APPDATA\LOCAL\MICROSOFT\VISUALSTUDIO\15.0_B94EA859\EXTENSIONS\DEJHVSWG.XKJ\TechTalk.SpecFlow.VisualStudio.CodeBehindGenerator.exe
Parameters: GenerateTestFile --featurefile C:\Users\HCHB\AppData\Local\Temp\tmpA463.tmp --outputdirectory C:\Users\HCHB\AppData\Local\Temp --projectsettingsfile C:\Users\HCHB\AppData\Local\Temp\tmpA453.tmp
Working Directory: 
