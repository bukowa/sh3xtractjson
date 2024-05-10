namespace SH3Textractor;

public static class Files {
    
    /// <summary>
    /// Recursively search a directory for files
    /// </summary>
    /// <param name="directoryPath"> The directory to search </param>
    /// <returns></returns>
    public static List<string> RecursiveFileSearch(string directoryPath) {
        
        // Get the files in the current directory
        string[] files    = Directory.GetFiles(directoryPath);
        var      fileList = files.ToList();

        // Get the subdirectories in the current directory
        string[] subDirectories = Directory.GetDirectories(directoryPath);
        foreach (string subDirectory in subDirectories) {
            
            // Recursively search each subdirectory
            var subDirectoryFiles = RecursiveFileSearch(subDirectory);
            fileList.AddRange(subDirectoryFiles);
        }

        return fileList;
    }
}