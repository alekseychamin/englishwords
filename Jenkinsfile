pipeline {
    agent any 
    stages {
        stage('Run tests'){
            steps {
                sh 'dotnet test'
            }
        }
        stages('Build'){
            steps {
                sh 'dotnet build --configuration Release'
            }
        }
    }
}