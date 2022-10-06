pipeline {
    agent any 
    stages {
        stage('Clean'){
            steps {
                cleanWs()
            }
        }
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