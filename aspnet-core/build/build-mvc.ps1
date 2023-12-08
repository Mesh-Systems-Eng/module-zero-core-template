echo " Welcome to docker build"
echo ""
echo ""

$ABP_MVC="abp/mvc"
$ABP_MVC_DOCKERFILE_PATH="src/AbpCompanyName.AbpProjectName.Web.Mvc/Dockerfile"
$ABP_NG="abp/ng"

# Add the user personal access token here that has access to Mesh.One nuget feed.
$PAT = ""

# Get the current directory
$currentDirectory = Get-Location

# Get the directory name
$directoryName = Split-Path $currentDirectory -Leaf

# Check if the directory name is 'build'
if ($directoryName -eq 'build') {
    # Change to the parent directory
    cd ..
}

echo " Building docker image $ABP_MVC..."
docker build --build-arg="FEED_ACCESSTOKEN=$PAT" -t $ABP_MVC -f $ABP_MVC_DOCKERFILE_PATH . 
echo " Done. -- Building docker image $ABP_MVC..."
echo ""
echo ""

# echo " Pushing docker image $ABP_MVC..."
# docker push $ABP_MVC
# echo " Done. -- Pushing docker image $ABP_MVC..."
# echo ""
# echo ""

# Return to the original directory
cd /
cd $currentDirectory
