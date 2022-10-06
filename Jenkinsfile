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
        stage('Docker build'){
            steps {
                sh 'docker build -t PublicApi .'
                sh 'docker run -d -p 8080:80 --name EnglishWordsApi PublicApi'
            }
        }
    }
}