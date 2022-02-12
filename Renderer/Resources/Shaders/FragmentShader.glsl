#version 430 core

//uniform sampler3D grid;
uniform vec3 cameraPosition;

layout(std430, binding = 3) buffer grid
{
    vec3 data_SSBO[];
};

in vec2 pos;
out vec4 outputColor;

float nearestDistanceToBox(vec3 point){
	vec3 boxSize = vec3(1);
	
	vec3 d = abs(point) - boxSize;

    float insideDistance = min(max(d.x, max(d.y, d.z)), 0);
    float outsideDistance = length(max(d, 0));

    return insideDistance + outsideDistance;
}

float rayMarching(vec3 startPoint, vec3 direction){
	for (int i = 0; i < 10; i++){
		float nearestDist = nearestDistanceToBox(startPoint);

		if (nearestDist < 0.1f){
			return 1;
		}

		startPoint += direction * nearestDist;
	}

	return 0;
}

void main()
{
	vec2 newPos = pos / 0.9;

	outputColor = vec4(rayMarching(vec3(newPos*3,-10), vec3(0,0,1))*data_SSBO[0], 1);
}