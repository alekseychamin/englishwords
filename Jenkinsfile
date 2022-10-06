pipeline {
    agent any 
    stages {
        stage('Run tests'){
            steps {
                sh 'dotnet test'
            }
        }
        stage('Build'){
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
    }
}