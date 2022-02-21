#version 430 core

uniform mat4 cameraView;

uniform vec2 cameraSize;
uniform vec2 cameraPlaneDist;
uniform bool cameraIsPersp;

uniform ivec3 voxelGridSize;
layout(std430, binding = 3) buffer voxelGrid
{
    vec3 data_SSBO[];
};

in vec2 screenPoint;

out vec4 outputColor;

//////////////////////

vec3 getRayStartPoint(){
	return (cameraView * vec4(screenPoint, 0, 1)).xyz;
}

vec3 getRayEndPoint(){
	vec3 endPoint;
	if (cameraIsPersp){
		endPoint = normalize(vec3(screenPoint, -cameraPlaneDist[0]));
	}
	else{
		endPoint = vec3(screenPoint, -1);
	}
	return (cameraView * vec4(endPoint, 1)).xyz;
}

void main()
{
	vec3 rayStartPoint = getRayStartPoint();
	vec3 rayEndPoint = getRayEndPoint();

	outputColor = vec4(abs(normalize(rayStartPoint - rayEndPoint)), data_SSBO[0] + voxelGridSize.x);
}