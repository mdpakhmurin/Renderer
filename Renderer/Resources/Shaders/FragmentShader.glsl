#version 430 core

uniform mat4 camera;

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
	float dist = 0;
	for (int i = 0; i < 10; i++){
		float nearestDist = nearestDistanceToBox(startPoint + dist*direction);
		
		if (nearestDist < 0.1f){
			return dist;
		}

		dist += nearestDist;
	}

	return 0;
}

void main()
{
	vec2 newPos = pos / 0.9;
	vec4 cameraPos = vec4(0, 0, -10, 0);

	vec3 startPoint = vec3(newPos*10, 0);
	vec3 endPoint = startPoint + vec3(0, 0, -1);

	startPoint = (camera * vec4(startPoint, 1)).xyz;
	endPoint = (camera * vec4(endPoint, 1)).xyz;

	float nearDist = rayMarching(startPoint, endPoint - startPoint);
	outputColor = vec4(nearDist*data_SSBO[0], 1);
}