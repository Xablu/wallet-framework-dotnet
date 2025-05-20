# Build the project
build:
	dotnet build -c Release

# Run tests
test:
	dotnet test

# Publish the project
publish:
	dotnet publish -c Release -o ./publish

# Clean the project
clean:
	dotnet clean