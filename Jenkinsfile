pipeline {
    agent any 
    stages {
        stage('Restore Nuget packages'){
            steps {
                sh 'dotnet restore'
            }
        }
    }
}