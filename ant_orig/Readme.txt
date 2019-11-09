The simulation we worked on is the scene World2.0 in the antw2 Unity project. Within this scene, object ThistlesReduced is the vegetation mesh model used in the original simulation. Thistleswithcolor is the mesh used in the final implementation, which is automatically divided into 173 parts by Unity. We divided them into 3 groups manually, as 0-57, 58-119 and 120-172, and then applied materials with different parameter settings to desynchronize the movements of vegetation in the wind. Ground is the ground mesh model. Sun is the direction light object as the light source in the scene. Clouds are the hemispherical objects used to simulate the cloud effect. 

Among the assets for this Unity project, the folder msc2017 contains all the assets used for the simulation in this project. In this folder, the folder Assetstore contains the plugins acquired from the Unity Asset Store. CapturePanorama helps us get the panoramic images used in the evaluation. TheTopicbirdTools can take screenshots inside the Unity Editor. The following is a documentation for all the scripts and shaders used in this project.


Scripts

suncal.cs
The script attached to the directional light object to set the light orientation according to the real-world solar position.

vertexloop.cs
The script used to find a specified mesh's lowest vertex height and set the pivot for wind animation as a material property according to this height.

normaltransformation.cs
The script used to update the vertex normals every frame.

assignmaterial.js
The script used to assign one material to multiple mesh objects.


Shaders

frontcolor.shader
The shader applied in the final implementation. Apply vertex color, cull the back faces and include the wind animation.

backcolor.shader
The shader applied in the final implementation. The only difference with frontcolor.shader is that it flips all the vertex normals and culls the front faces. The values of material properties should be consistent with frontcolor.shader.

frontcolorstatic.shader
Apply vertex color, cull the back faces. 

backcolorstatic.shader
The only difference with frontcolorstatic.shader is that it flips all the vertex normals and culls the front faces. The values of material properties should be consistent with frontcolorstatic.shader.

dissolving.shader
The shader applied in the original simulation.

frontoriginal.shader
Based on the dissolving.shader, cull the back faces and include wind animation.

backoriginal.shader
The only difference with frontoriginal.shader is that it flips all the vertex normals and culls the front faces. The values of material properties should be consistent with frontoriginal.shader.

cloud.shader
The shader applied to the hemispherical object to simulate the cloud effect, include the lighting and cloud shadows. The four gray scale textures are acquired from asset [BFW]SimpleDynamicClouds in the Unity Asset Store.

visualizeuv.shader
The shader used to visualize the UV coordinates of the mesh model. UV coordinates are visualized as red and green channel of the RGB color. The code is from https://docs.unity3d.com/Manual/SL-VertexProgramInputs.html.

visualizenormal.shader
The shader used to visualize the vertex normals of the mesh model. The x,y and z components of the normals are visualized as RGB colors. The code is from https://docs.unity3d.com/Manual/SL-VertexProgramInputs.html.

visualizetangent.shader
The shader used to visualize the tangent vectors of the mesh model. The x,y and z components of the tangent vectors are visualized as RGB colors. The code is from https://docs.unity3d.com/Manual/SL-VertexProgramInputs.html.
