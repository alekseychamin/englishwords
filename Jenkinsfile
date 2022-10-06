pipeline {
    agent any 
    stages {
        stage('Clean and checkout'){
            steps {
                cleanWs()
                checkout scm
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