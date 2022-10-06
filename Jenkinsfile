pipeline {
    agent any 
    stages {
        stage('Build'){
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
        stage('Run tests'){
            steps {
                sh 'dotnet test'
            }
        }
    }
}